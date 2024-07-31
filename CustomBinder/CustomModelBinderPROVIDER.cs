using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using NetCoreProyectExample.Models;

namespace NetCoreProyectExample.CustomBinder
{
    public class CustomModelBinderPROVIDER : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(BookModel))
            {
                return new BinderTypeModelBinder(typeof(CustomModelExample));
            }
            return null;
        }
    }
}
