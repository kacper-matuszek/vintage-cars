using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.Common;
using VintageCars.Domain.Country.Commands;
using VintageCars.Domain.Country.Response;
using VintageCars.Domain.Country.StateProvince.Commands;
using VintageCars.Domain.Country.StateProvince.Response;
using VintageCars.Domain.Utils;

namespace VintageCars.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CountryController : BaseController
    {
        public CountryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("all")]
        public async Task<ActionResult<PagedList<CountryView>>> GetAll([FromQuery] PagedRequest pageInfo)
            => Single(await SendAsync(new GetAllCommand(pageInfo)));

        [HttpGet("state-province/all/{countryId:guid}")]
        public async Task<ActionResult<PagedList<StateProvinceView>>> GetAll(Guid countryId, [FromQuery] PagedRequest pageInfo)
            => Single(await SendAsync(new GetAllStateProvinceCommand(countryId, pageInfo)));

    }
}
