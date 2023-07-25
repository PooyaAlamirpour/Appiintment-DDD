using System;
using System.Threading;
using System.Threading.Tasks;
using Charisma.Application.Appointments.Queries.GetAppointments;
using Charisma.Application.Salary.Queries;
using Charisma.Contracts.Appointments.Requests;
using Charisma.Contracts.Appointments.Responses;
using Charisma.Contracts.Common;
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