using System;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Common.Interfaces.Persistence;
using Charisma.Application.Common.Messages;
using Charisma.Application.Common.Models;
using Charisma.Domain.Core.Appointments;
using ErrorOr;

namespace Charisma.Application.Appointments.Queries.GetAppointments
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