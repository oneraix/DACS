using Microsoft.AspNetCore.Mvc;

namespace DACS.Controllers
{
    public class ClassController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
