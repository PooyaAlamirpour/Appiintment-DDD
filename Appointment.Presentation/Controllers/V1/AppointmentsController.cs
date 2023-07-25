using System.Threading;
using System.Threading.Tasks;
using Appointment.Application.Appointments.Commands.CreateAppointment;
using Appointment.Application.Appointments.Commands.CreateEarliestAppointment;
using Appointment.Application.Appointments.Commands.UpdateAppointment;
using Appointment.Application.Appointments.Queries.GetAppointments;
using Appointment.Contracts.Appointments.Requests;
using Appointment.Contracts.Appointments.Responses;
using Appointment.Contracts.Common;
using Appointment.Contracts.Routes;
using Appointment.Infrastructure.Authentication.Constants;
using Appointment.Presentation.Common.Mappings.Abstracts;
using Appointment.Presentation.Controllers.Base;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Appointment.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AppointmentsController : ApiController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IDtoConvertor _convertor;

        public AppointmentsController(ISender sender, IMapper mapper, IDtoConvertor convertor)
        {
            _sender = sender;
            _mapper = mapper;
            _convertor = convertor;
        }

        [HttpGet(ApiRoutes.Appointment.Get)]
        [Authorize(Policy = PolicyNames.RequireExpertPolicy)]
        [ProducesResponseType(typeof(PagedResponse<AppointmentListResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetAppointmentsQueryParameters queryParameters,
            CancellationToken cancellationToken = default)
        {
            var query = _mapper.Map<GetAppointmentsQuery>(queryParameters);

            var result = await _sender.Send(query, cancellationToken);

            return result.Match(appointments => 
                    Ok(_convertor.ToDto(appointments)),
                Problem);
        }

        [HttpPost(ApiRoutes.Appointment.Create)]
        [ProducesResponseType(typeof(CreateAppointmentResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> SetAppointment(CreateAppointmentRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = _mapper.Map<CreateAppointmentCommand>(request);
            var result = await _sender.Send(command, cancellationToken);

            return result.Match(appointment =>
                    CreatedAtAction(actionName: nameof(SetAppointment),
                        routeValues: new {AppointmentId = appointment.Value},
                        value: new {AppointmentId = appointment.Value}),
                Problem);
        }
        
        [HttpPost(ApiRoutes.Appointment.CreateEarliestAppointment)]
        [ProducesResponseType(typeof(CreateAppointmentResponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> SetEarliestAppointment(CreateEarliestAppointmentRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = _mapper.Map<CreateEarliestAppointmentCommand>(request);
            var result = await _sender.Send(command, cancellationToken);

            return result.Match(appointment =>
                    CreatedAtAction(actionName: nameof(SetAppointment),
                        routeValues: new {AppointmentId = appointment.Value},
                        value: new {AppointmentId = appointment.Value}),
                Problem);
        }
        

        [HttpPut(ApiRoutes.Appointment.Update)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(string trackingCode, [FromBody] UpdateAppointmentRequest request,
            CancellationToken cancellationToken = default)
        {
            UpdateAppointmentCommand command = _convertor.ToCommand(trackingCode.ToString(), request);

            var result = await _sender.Send(command, cancellationToken);

            return result.Match(_ => NoContent(), Problem);
        }
    }
}

