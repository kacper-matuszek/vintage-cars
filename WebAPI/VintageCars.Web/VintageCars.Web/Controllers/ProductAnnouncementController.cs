using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.Common;
using VintageCars.Domain.ProductAnnouncement.Commands;
using VintageCars.Domain.ProductAnnouncement.Queries;
using VintageCars.Domain.ProductAnnouncement.Response;
using VintageCars.Domain.Utils;

namespace VintageCars.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductAnnouncementController : BaseController
    {
        public ProductAnnouncementController(IMediator mediator) : base(mediator)
        {
        }

        [Authorize(Roles = "Registered")]
        [HttpPost("create")]
        public async Task<ActionResult> CreateProductAnnouncement([FromBody] CreateProductAnnouncement productAnnouncement)
            => await ExecuteCommandWithoutResult(productAnnouncement);

        [HttpGet("list")]
        public async Task<ActionResult<PagedList<ProductAnnouncementShortInfoView>>> ProductAnnouncementsShortInfo([FromQuery] PagedRequest request)
        => Result(await SendAsync(new GetProductAnnouncementsShortInfoQuery(request)));
    }
}
