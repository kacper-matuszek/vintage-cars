using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.Catalog.Queries;
using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Common;
using VintageCars.Domain.Utils;

namespace VintageCars.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }


        [Authorize(Roles = "Registered")]
        [HttpGet("list")]
        public async Task<ActionResult<PagedList<CategoryShortInfoView>>> CategoriesShortInfo([FromQuery] PagedRequest pagedRequest)
            => Result(await SendAsync(new GetCategoriesShortInfoQuery(pagedRequest)));

        [Authorize(Roles = "Registered")]
        [HttpGet("attribute/list")]
        public async Task<ActionResult<List<CategoryAttributeFullInfoView>>> Attributes([FromQuery] GetCategoryAttributesWithValuesQuery query)
            => Result(await SendAsync(query));
    }
}
