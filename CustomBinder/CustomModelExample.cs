using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCoreProyectExample.Controllers;
using NetCoreProyectExample.Models;

namespace NetCoreProyectExample.CustomBinder
{
    public class CustomModelExample : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            BookModel book = new BookModel();
            if (bindingContext.ValueProvider.GetValue("BookId").Count()>0)
            {
                book.BookId = Convert.ToInt32(bindingContext.ValueProvider.GetValue("BookId").FirstValue);
            }
            if (bindingContext.ValueProvider.GetValue("Author").Count() > 0)
            {
                book.Author = bindingContext.ValueProvider.GetValue("Author").FirstValue;
            }

            bindingContext.Result = ModelBindingResult.Success(book) ;
            return Task.CompletedTask;
        }
    }
}
