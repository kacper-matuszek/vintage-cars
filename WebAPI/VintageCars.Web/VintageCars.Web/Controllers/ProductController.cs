using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VintageCars.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        public ProductController(IMediator mediator) : base(mediator)
        {
        }
    }
}
