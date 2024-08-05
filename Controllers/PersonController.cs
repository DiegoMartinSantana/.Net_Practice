using Microsoft.AspNetCore.Mvc;
using NetCoreProyectExample.Models;

namespace NetCoreProyectExample.Controllers
{
    public class PersonController : Controller
    {
        [Route("[controller]/Home")]
        public IActionResult Index()
        {
            PersonModel personModel = new PersonModel();
            personModel.Age = 25;
            personModel.Name = "Juan";
            personModel.Nationality = "USA";
            List<PersonModel?> collection = new List<PersonModel?>();
            collection.Add(personModel);
            collection.Add(new PersonModel { Age = 30, Nationality = "ARG", Name = "Carlos" });
            collection.Add(new PersonModel { Age = 35, Nationality = "BRA", Name = "Pedro" });
            ViewData["collectionPeople"] = collection;
            string title = "hi from the controller!!";
            ViewData["pageTitle"] = title;
            //return a object of view class.
          return View("Home",collection);
          
        }

        [Route("[controller]/Details")]
        public IActionResult Details([FromQuery]string? name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Name needs supplied");
            }
            PersonModel personModel = new PersonModel();
            personModel.Age = 25;
            personModel.Name = "Juan";
            personModel.Nationality = "USA";
            List<PersonModel?> collection = new List<PersonModel?>();
            collection.Add(personModel);
            collection.Add(new PersonModel { Age = 30, Nationality = "ARG", Name = "Carlos" });
            collection.Add(new PersonModel { Age = 35, Nationality = "BRA", Name = "Pedro" });
            collection.Add(new PersonModel { Age = 24 ,Nationality="ARG",Name="Diego"});
          PersonModel? PersonMatch =  collection.Where(x => Equals(x.Name,name)).FirstOrDefault();

            return View(PersonMatch);
        }

    }
}
