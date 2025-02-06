using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }
        [Route("")]
        public RedirectResult Index()
        {
            return Redirect("https://pawprint.sebastijanzindl.me");
        }

        [Route("/api/status")]
        public ActionResult Status()
        {
            return new JsonResult(new { health = "Working" });
        }
    }
}
