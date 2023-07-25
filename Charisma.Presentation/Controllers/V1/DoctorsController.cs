using System;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Doctors.Commands;
using Charisma.Contracts.Common;
using Charisma.Contracts.Doctors;
using Charisma.Contracts.Routes;
using Charisma.Infrastructure.Authentication.Constants;
using Charisma.Presentation.Common.Mappings.Abstracts;
using Charisma.Presentation.Controllers.Base;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Charisma.Presentation.Controllers.V1
{
    [ApiVersion("1.0")]
    public class DoctorsController : ApiController
    {
        private readonly ISender _sender;
        private readonly IDtoConvertor _convertor;
        private readonly IMapper _mapper;

        public DoctorsController(ISender sender, IDtoConvertor convertor, IMapper mapper)
        {
            _sender = sender;
            _convertor = convertor;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Doctor.Get)]
        [Authorize(Policy = PolicyNames.RequireExpertPolicy)]
        [ProducesResponseType(typeof(PagedResponse<DoctorResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] GetDoctorsQueryParameters queryParameters,
            CancellationToken cancellationToken = default)
        {
            var query = _convertor.ToQuery(queryParameters);
            var result = await _sender.Send(query, cancellationToken);
            
            return result.Match(doctors => 
                    Ok(_convertor.ToDto(doctors)),
                Problem);
        }
        
        [HttpPost(ApiRoutes.Doctor.Create)]
        [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(DefineDoctorRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = _convertor.ToCommand(request);
            var result = await _sender.Send(command, cancellationToken);

            return result.Match(doctor => CreatedAtAction(
                    actionName: nameof(Create),
                    routeValues: new {DoctorId = doctor.DoctorId.Value},
                    value: new {DoctorId = doctor.DoctorId.Value}),
                Problem);
        }
        
        [HttpPost(ApiRoutes.Doctor.CreateDoctorSchedule)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSchedule(Guid doctorId, CreateDoctorScheduleRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = _convertor.ToCommand(doctorId, request);
            var result = await _sender.Send(command, cancellationToken);

            return result.Match(doctor => CreatedAtAction(
                    actionName: nameof(Create),
                    routeValues: new {IsCreated = result.Value},
                    value: new {IsCreated = result.Value}),
                Problem);
        }
    }
}