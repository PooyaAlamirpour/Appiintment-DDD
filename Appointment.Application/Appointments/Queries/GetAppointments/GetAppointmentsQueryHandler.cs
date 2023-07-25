using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Common.Interfaces.Persistence;
using Appointment.Application.Common.Messages;
using Appointment.Application.Common.Models;
using Appointment.Domain.Core.Appointments;
using ErrorOr;

namespace Appointment.Application.Appointments.Queries.GetAppointments
{
    internal sealed class GetAppointmentsQueryHandler : IQueryHandler<GetAppointmentsQuery, ErrorOr<Paged<AppointmentAggregateRoot>>>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public GetAppointmentsQueryHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ErrorOr<Paged<AppointmentAggregateRoot>>> Handle(GetAppointmentsQuery request,
            CancellationToken cancellationToken)
        {
            var appointments = await _appointmentRepository.FindManyWithPaginationAsync(
                request.DoctorId, request.PatientId, request.AppointmentStart, request.AppointmentEnd, request.TrackingCode, 
                request.PageSize, request.PageNumber, cancellationToken);

            return appointments;
        }
    }
}