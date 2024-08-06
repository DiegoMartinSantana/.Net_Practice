using Microsoft.AspNetCore.Mvc;

namespace NetCoreProyectExample.Controllers
{
    public class HomeProductsController : Controller
    {
        [Route("[controller]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("[controller]/About")]
        public IActionResult About()
        {
            return View();
        }
        [Route("[controller]/Contact")]

        public IActionResult Contact()
        {
            return View();
        }


    }
}
