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
        public async Task<ActionResult> CreateOrUpdate([FromBody] CreateCategoryAttributeCommand categoryAttributeCommand)
            => await ExecuteCommandWithoutResult(categoryAttributeCommand);
    }
}
