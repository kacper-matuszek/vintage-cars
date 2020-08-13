using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VintageCars.Domain.Exceptions;
using VintageCars.Domain.Exceptions.Response;

namespace VintageCars.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorDetails Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;

            var code = exception switch
            {
                ResourcesNotFoundException _ => 404,
                _ => 500,
            };

            Response.StatusCode = code; 

            return new ErrorDetails(exception, code); 
        }
    }
}
