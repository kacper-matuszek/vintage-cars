using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.Catalog.Commands;

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
        [HttpPost("attribute-value/link")]
        public async Task<ActionResult> LinkCategoryAttributeValue([FromBody] LinkCategoryAttributeValueCommand linkCategoryAttributeValueCommand)
            => await ExecuteCommandWithoutResult(linkCategoryAttributeValueCommand);
    }
}
