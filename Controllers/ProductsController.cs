using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using NetCoreProyectExample.Models;

namespace NetCoreProyectExample.Controllers
{
    public class ProductsController : Controller
    {
        [Route("[controller]")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("[controller]/Search")]
        public IActionResult Search([FromQuery] int? id)
        {
            if (id != null)
            {
                ViewBag.Id = id;
            }
            return View();
        }
        [Route("[controller]/Order")]
        public IActionResult Order()
        {
            return View();
        }

        [Route("[controller]/SomePartial")]
        public IActionResult GetSomethingPartial()
        {
            var list =new ListModel()
            {
                ListTitle = "Countries",
                ListItems = new List<string> {"Argentina","Usa","pepelandia","Georgia"}
            };
            return PartialView("_SomePartialView", list);
        }
    }
}
