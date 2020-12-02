using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.Catalog.Commands;
using VintageCars.Domain.Catalog.Queries;
using VintageCars.Domain.Catalog.Response;
using VintageCars.Domain.Common;
using VintageCars.Domain.Utils;

namespace VintageCars.Web.Controllers.Admin
{
    [Authorize(Roles = "Administrators")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrUpdate([FromBody] CreateUpdateCategoryCommand updateCategoryCommand)
            => await ExecuteCommandWithoutResult(updateCategoryCommand);

        [HttpGet("list")]
        public async Task<ActionResult<PagedList<CategoryView>>> Categories([FromQuery] PagedRequest pagedRequest)
            => Single(await SendAsync(new GetCategoriesQuery(pagedRequest)));

        [HttpPost("delete")]
        public async Task<ActionResult> DeleteCategory([FromBody] DeleteCategoryCommand deleteCategoryCommand)
            => await ExecuteCommandWithoutResult(deleteCategoryCommand);

        [HttpPost("attribute")]
        public async Task<ActionResult> CreateOrUpdateAttribute([FromBody] CreateUpdateCategoryAttributeCommand updateCategoryAttributeCommand)
            => await ExecuteCommandWithoutResult(updateCategoryAttributeCommand);

        [HttpPost("attribute/delete")]
        public async Task<ActionResult> DeleteCategoryAttribute([FromBody] DeleteCategoryAttributeCommand deleteCategoryAttributeCommand)
            => await ExecuteCommandWithoutResult(deleteCategoryAttributeCommand);

        [HttpGet("attribute/list")]
        public async Task<ActionResult<PagedList<CategoryAttributeView>>> GetList([FromQuery] PagedRequest pageInfo)
            => Single(await SendAsync(new GetCategoryAttributesCommand(pageInfo)));

        [HttpPost("attribute-value/link")]
        public async Task<ActionResult> LinkCategoryAttributeValue([FromBody] LinkCategoryAttributeValueCommand linkCategoryAttributeValueCommand)
            => await ExecuteCommandWithoutResult(linkCategoryAttributeValueCommand);

        [HttpPost("attribute-value/delete")]
        public async Task<ActionResult> DeleteCategoryAttributevalue([FromBody] DeleteCategoryAttributeValueCommand deleteCategoryCommand)
            => await ExecuteCommandWithoutResult(deleteCategoryCommand);
    }
}
