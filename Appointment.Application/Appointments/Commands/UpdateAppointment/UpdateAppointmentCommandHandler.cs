using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Interfaces.Infrastructure;
using Appointment.Application.Common.Interfaces.Persistence;
using Appointment.Application.Common.Messages;
using Appointment.Domain.Core.Appointments;
using Appointment.Domain.Core.Appointments.ValueObjects;
using Appointment.Domain.GenericCore.Errors;
using Appointment.Domain.GenericCore.Extensions;
using ErrorOr;

namespace Appointment.Application.Appointments.Commands.UpdateAppointment
{
    public sealed class UpdateAppointmentCommandHandler : ICommandHandler<UpdateAppointmentCommand, ErrorOr<Updated>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentAggregateRoot _appointmentAggregateRoot;
        private readonly INotificationFactory _notificationFactory;

        public UpdateAppointmentCommandHandler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork, 
            IDoctorRepository doctorRepository, IAppointmentAggregateRoot appointmentAggregateRoot, INotificationFactory notificationFactory)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _doctorRepository = doctorRepository;
            _appointmentAggregateRoot = appointmentAggregateRoot;
            _notificationFactory = notificationFactory;
        }

        public async Task<ErrorOr<Updated>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var requestedAppointment = await _appointmentRepository.GetByTrackingCodeAsync(
                request.TrackingCode, cancellationToken);
            
            if (requestedAppointment is null) return Errors.Appointment.NotFound(request.TrackingCode);
            
            var doctorValueObject = await GetDoctor(requestedAppointment);

            var appointmentHistories = await GetPatientHistories(request, requestedAppointment);

            var appointment = AppointmentAggregateRoot
                .Define(requestedAppointment.DoctorId.Value,  requestedAppointment.PatientId.Value, doctorValueObject, 
                    request.AppointmentStartDateTime, request.DurationMinutes)
                .SetPatientAppointmentHistory(appointmentHistories);
            
            await _appointmentAggregateRoot.Do(appointment);
            appointment.SetId(request.TrackingCode);
            
            try
            {
                await _appointmentRepository.DeleteByTrackingCode(request.TrackingCode);
                await _appointmentRepository.AddAsync(appointment, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                
                var notification = _notificationFactory.Make(NotificationTypes.EMAIL);
                await notification.SendNotificationAsync(requestedAppointment.PatientId.Value.ToString(), "Updating Appointment", 
                    $"Appointment of {appointment.TrackingCode} is updated to {request.AppointmentStartDateTime}.", cancellationToken);
                
                return Result.Updated;
            }
            catch (Exception ex)
            {
                return Errors.Appointment.Exception(ex.Message);
            }

            return ErrorOr<Updated>.From(new List<Error>()
            {
                Error.Failure("0x12", "There is an issue in updating appointment")
            });
        }

        private async Task<List<AppointmentHistoryValueObject>?> GetPatientHistories(UpdateAppointmentCommand request, AppointmentAggregateRoot? requestedAppointment)
        {
            var patientAppointmentsInDay =
                await _appointmentRepository.GetPatientAppointmentsInDay(requestedAppointment.PatientId.Value,
                    request.AppointmentStartDateTime.ToDateOnly());

            var appointmentHistories = patientAppointmentsInDay.Select(x =>
                    AppointmentHistoryValueObject.New(x.AppointmentTime, x.AppointmentDuration))
                .ToList();
            return appointmentHistories;
        }

        private async Task<DoctorValueObject?> GetDoctor(AppointmentAggregateRoot? requestedAppointment)
        {
            var doctor = await _doctorRepository.GetByIdAsync(requestedAppointment.DoctorId.Value);
            var doctorValueObject = DoctorValueObject.New(doctor.Speciality, doctor.Schedule);
            return doctorValueObject;
        }
    }
}