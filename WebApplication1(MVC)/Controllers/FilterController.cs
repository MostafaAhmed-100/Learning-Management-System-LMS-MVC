using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1_MVC_.Filter;

namespace WebApplication1_MVC_.Controllers
{
    public class FilterController : Controller
    {
        [MyCustomExceptionFilter]
        public IActionResult Index()
        {
            throw new Exception("This is a test exception for demonstration purposes.");
        }
    }
}
