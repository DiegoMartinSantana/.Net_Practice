using Microsoft.AspNetCore.Mvc;
using NetCoreProyectExample.Models;

namespace NetCoreProyectExample.Controllers
{
    public class ECommerceController : Controller
    {
        [Route("[controller]/CreateOrder")]
        [HttpPost]
        public IActionResult CreateOrder(OrderModel order)
        {

            if (ModelState.IsValid)
            {
                return Ok(order.OrderNo);
            }
            else
            {
                string Errors = string.Join("\n",ModelState.Values.SelectMany(x => x.Errors).Select(errors => errors.ErrorMessage));

                return BadRequest(Errors);
            }

        }
    }
}
