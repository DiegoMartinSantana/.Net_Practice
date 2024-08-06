using Microsoft.AspNetCore.Mvc;
using NetCoreProyectExample.Models;

namespace NetCoreProyectExample.Controllers
{
    public class WeatherHomeWork10 : Controller
    {
        [Route("[controller]/")]
        public IActionResult Index()
        {
            List<CityWeather> ListWeather = new List<CityWeather>();
            ListWeather.Add(new CityWeather { CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"), TemperatureFahrenheit = 33 });
            ListWeather.Add(new CityWeather { CityUniqueCode = "NYC", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"), TemperatureFahrenheit = 60 });
            ListWeather.Add(new CityWeather { CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"), TemperatureFahrenheit = 82 });

            return View(ListWeather);

        }

        [Route("[controller]/Details/{cityCode?}")]
        public IActionResult Details(string? cityCode)
        {
            if (cityCode == null) return BadRequest("City cant not empty");

            List<CityWeather> ListWeather = new List<CityWeather>();
            ListWeather.Add(new CityWeather { CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"), TemperatureFahrenheit = 33 });
            ListWeather.Add(new CityWeather { CityUniqueCode = "NYC", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"), TemperatureFahrenheit = 60 });
            ListWeather.Add(new CityWeather { CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"), TemperatureFahrenheit = 82 });
            CityWeather CityMatch = ListWeather.Where(x => Equals(x.CityUniqueCode, cityCode)).FirstOrDefault();

            if (CityMatch != null)
            {
                return View(CityMatch);
            }
            else
            {
                return View();
            }
        }
    }
}
