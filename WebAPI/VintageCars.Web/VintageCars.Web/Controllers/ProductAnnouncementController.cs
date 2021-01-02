using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VintageCars.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductAnnouncementController : BaseController
    {
        public ProductAnnouncementController(IMediator mediator) : base(mediator)
        {
        }
    }
}
