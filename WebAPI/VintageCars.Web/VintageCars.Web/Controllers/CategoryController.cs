using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.Catalog.Commands;
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

        [Authorize(Roles = "Administrators")]
        [HttpPost]
        public async Task<ActionResult> CreateOrUpdate([FromBody] CreateUpdateCategoryCommand updateCategoryCommand)
            => await ExecuteCommandWithoutResult(updateCategoryCommand);

        [Authorize(Roles = "Administrators")]
        [HttpPost("attribute")]
        public async Task<ActionResult> CreateOrUpdateAttribute([FromBody] CreateUpdateCategoryAttributeCommand updateCategoryAttributeCommand)
            => await ExecuteCommandWithoutResult(updateCategoryAttributeCommand);

        [Authorize(Roles = "Administrators")]
        [HttpGet("attribute/list")]
        public async Task<ActionResult<PagedList<CategoryAttributeView>>> GetList([FromQuery] PagedRequest pageInfo)
            => Single(await SendAsync(new GetCategoryAttributesCommand(pageInfo)));

        [Authorize(Roles = "Administrators")]
        [HttpPost("attribute-value/link")]
        public async Task<ActionResult> LinkCategoryAttributeValue([FromBody] LinkCategoryAttributeValueCommand linkCategoryAttributeValueCommand)
            => await ExecuteCommandWithoutResult(linkCategoryAttributeValueCommand);

        [Authorize(Roles = "Administrators")]
        [HttpPost("attribute-value/delete")]
        public async Task<ActionResult> DeleteCategoryAttributevalue([FromBody] DeleteCategoryAttributeValueCommand deleteCategoryCommand)
            => await ExecuteCommandWithoutResult(deleteCategoryCommand);
    }
}
