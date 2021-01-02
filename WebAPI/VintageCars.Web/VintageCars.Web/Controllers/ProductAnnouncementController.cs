using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.ProductAnnouncement.Commands;

namespace VintageCars.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductAnnouncementController : BaseController
    {
        public ProductAnnouncementController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateProductAnnouncement([FromBody] CreateProductAnnouncement productAnnouncement)
            => await ExecuteCommandWithoutResult(productAnnouncement);
    }
}
