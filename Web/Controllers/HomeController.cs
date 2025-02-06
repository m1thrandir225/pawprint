using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return new JsonResult(new { message = "Hello World!" });
        }
    }
}
