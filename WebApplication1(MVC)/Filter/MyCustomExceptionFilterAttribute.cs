using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication1_MVC_.Filter
{
    public class MyCustomExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            
            var viewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), context.ModelState);

            viewData["ErrorMessage"] = context.Exception.Message;

            context.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = viewData
            };

            context.ExceptionHandled = true;
        }
    }
}
    