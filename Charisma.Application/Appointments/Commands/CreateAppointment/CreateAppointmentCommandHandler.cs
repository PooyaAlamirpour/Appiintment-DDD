using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Application.Common.Messages;
using Charisma.Domain.Core.Appointments;
using Charisma.Domain.Core.Appointments.ValueObjects;
using Charisma.Domain.GenericCore.Errors;
using Charisma.Domain.GenericCore.Extensions;
using ErrorOr;

namespace Charisma.Application.Appointments.Commands.CreateAppointment
{
    public sealed class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand, ErrorOr<AppointmentIdValueObject>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IAppointmentAggregateRoot _appointmentAggregateRoot;
        
        public CreateAppointmentCommandHandler(IAppointmentAggregateRoot appointment, IDoctorRepository doctorRepository, 
            IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentAggregateRoot = appointment;
            _doctorRepository = doctorRepository;
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<AppointmentIdValueObject>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.DoctorId);
            var doctorValueObject = DoctorValueObject.New(doctor.Speciality, doctor.Schedule);

            var doctorAppointments = await _appointmentRepository.GetDoctorAppointmentsFromNow(request.DoctorId);
            var doctorAppointmentsHistory = doctorAppointments.Select(x =>
                    AppointmentHistoryValueObject.New(x.AppointmentTime, x.AppointmentDuration))
                .ToList();
            
            var patientAppointmentsInDay = 
                await _appointmentRepository.GetPatientAppointmentsInDay(request.PatientId, request.StartDateTime.ToDateOnly());
            
            var patientAppointmentHistories = patientAppointmentsInDay.Select(x =>
                AppointmentHistoryValueObject.New(x.AppointmentTime, x.AppointmentDuration))
                .ToList();

            var appointment = AppointmentAggregateRoot
                .Define(request.DoctorId,  request.PatientId, doctorValueObject, request.StartDateTime, request.DurationMinutes)
                .SetPatientAppointmentHistory(patientAppointmentHistories)
                .SetDoctorAppointmentHistory(doctorAppointmentsHistory);
            
            var appointmentId = await _appointmentAggregateRoot.Do(appointment);
            appointment.SetId(appointmentId);
            try
            {
                await _appointmentRepository.AddAsync(appointment, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return Errors.Appointment.Exception(ex.Message);
            }
            
            return appointmentId;
        }

    }
}