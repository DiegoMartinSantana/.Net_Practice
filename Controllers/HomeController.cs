using Microsoft.AspNetCore.Mvc;
using NetCoreProyectExample.Models;

namespace NetCoreProyectExample.Controllers
{
    public class HomeController : Controller
    {
        #region Controllers (6)
        //access to the same method
        [Route("sayhello")]
        [Route("sayhello2")]
        //default url
        public string method1() => "Hello from method1";

        [HttpGet]
        [Route("Person")]
        public JsonResult Person()
        {
            PersonModel pepe = new PersonModel()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Nationality = "Argentinian",
                Age = 30
            };
            return Json(pepe);
        }

        [Route("Home")]
        [Route("/")]

        public ContentResult Index()
        {
            return Content("Hi from index, im  a Content result", "text/plain");
        }
        //   public string Index() => "Home Default";

        [Route("About")]
        public string About() => "About ";


        [Route("Contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact() => "Contact PAge";

        //files 
        [Route("Donwload-Virtual")]
        public VirtualFileResult VirtualFile()
        {
            return File("Cv.pdf", "application/pdf");
        }
        [Route("Donwload-Physical")]
        public PhysicalFileResult PhysicalFile()
        {
            return PhysicalFile(@"c:\Users\mante\Desktop\Presentacion - Diego Santana.pdf", "application/pdf");

        }
        [Route("Donwload-File")]
        public FileContentResult FileExample()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"c:\Users\mante\Desktop\Presentacion - Diego Santana.pdf");
            //   return  new FileContentResult(bytes,"application/pdf");
            return File(bytes, "application/pdf");
        }

        //ALL CAN BE REEMPLAZED FOR I ACTION RESULT . THE PARENT INTERFACE

        [Route("Login{log:bool=false}")]
        public IActionResult Logged()
        {
            if (!Request.Query.ContainsKey("log"))
            {
                return BadRequest("Not login");
            }
            PersonModel people = new PersonModel()
            {
                Id = Guid.NewGuid(),
                Name = "Test",
                Nationality = "Arg",
                Age = 30
            };
            return Ok(people);

        }
        #endregion

    }
}
