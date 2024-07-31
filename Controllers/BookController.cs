using Microsoft.AspNetCore.Mvc;
using NetCoreProyectExample.CustomBinder;
using NetCoreProyectExample.Models;

namespace NetCoreProyectExample.Controllers
{
    public class BookController : Controller
    {
        #region ModelBinding (7)

        [Route("[controller]/Index")]
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpPost]
        [Route("[controller]/CreateTwo")]
        //using custom model BINDER
        public IActionResult CreateTwo([ModelBinder(typeof(CustomModelExample))] [FromBody] BookModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(model);
            }
            else
            {
                return BadRequest();
            }
        }
        //using custom model BINDER PROVIDER
        public IActionResult CreateThree( BookModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(3);
            }
            else
            {
                return BadRequest();
            }
        }

        //RECEIVE FROM BODY
        [HttpPost]
        [Route("[controller]/Create")]
        public IActionResult CreateBodyBook([Bind(nameof(BookModel.Author),nameof(BookModel.BookId),
            nameof(BookModel.Description))] [FromBody] BookModel book ) //receive json or xml ( from body)
        {
            if (!ModelState.IsValid)
            {


                /*
                foreach (var item in ModelState.Values)
                {
                    foreach (var errors in item.Errors)
                    {
                        ListErrors.Add(errors.ErrorMessage);
                    }
                }
                */
                string errors = string.Join("\n", ModelState.Values.SelectMany(x => x.Errors) //select these
                     .Select(errors => errors.ErrorMessage));
                //and in base of these select this.
                return BadRequest(errors);
            }


            /* 
            if(book is null)
            {
                return BadRequest("book not suplied");
            }
            */
            return Ok(book.ToString());


        }
        //RECEIVE FROM QUERY ?=..
        [HttpPost]
        [Route("[controller]/CreateInQuery")]
        //CreateInQuery?BookId=2&Author=pepe
        public IActionResult CreateQueryBook([FromQuery] BookModel book)
        {

            if (book is null)
            {
                return BadRequest("book not suplied");
            }
            return Ok(book.ToString());


        }
        //RECEIVE FROM ROUTE /.../...
        [HttpPost]
        [Route("[controller]/CreateInRoute/{bookId:int?}/{author?}")]
        //Book/CreateInRoute/1/pepe
        public IActionResult CreateRouteBook([FromRoute] BookModel book)
        {

            if (book is null)
            {
                return BadRequest("book not suplied");
            }
            return Ok(book.ToString());


        }


        #endregion
    }
}
