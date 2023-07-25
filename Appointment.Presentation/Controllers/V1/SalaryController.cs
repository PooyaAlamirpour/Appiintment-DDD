using System;
using System.Threading;
using System.Threading.Tasks;
using Appointment.Contracts.Appointments.Requests;
using Appointment.Contracts.Appointments.Responses;
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
    public class SalaryController : ApiController
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly IDtoConvertor _convertor;

        public SalaryController(ISender sender, IMapper mapper, IDtoConvertor convertor)
        {
            _sender = sender;
            _mapper = mapper;
            _convertor = convertor;
        }
        
        [HttpGet(ApiRoutes.Salary.GetById)]
        [Authorize(Policy = PolicyNames.RequireExpertPolicy)]
        [ProducesResponseType(typeof(SalaryResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById(Guid doctorId, [FromQuery] GetDoctorSalaryQueryParameters queryParameters,
            CancellationToken cancellationToken = default)
        {
            var query = _convertor.ToQuery(doctorId, queryParameters);

            var result = await _sender.Send(query, cancellationToken);

            return result.Match(salary => 
                    Ok(_convertor.ToDto(salary)),
                Problem);
        }
    }
}