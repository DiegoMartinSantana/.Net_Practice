using Microsoft.AspNetCore.Mvc;

namespace NetCoreProyectExample.Controllers
{
    [Route("[controller]")]
    public class ModelBindingController : Controller
    {
        [Route("/Index")]
        public IActionResult Index()
        {
            return View();
        }
        #region  ModelBinding 
        [Route("/Hi")]
        public IActionResult Hi(string name,bool islog)
        {
            if (string.IsNullOrEmpty(name) || !islog)
            {
                return BadRequest();
            }
            return Json($"Hi,{name}");
        }


        #endregion
    }
}
