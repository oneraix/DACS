using DACS.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DACS.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassRepository _classRepository;
        public ClassController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        public async Task<IActionResult> Index()
        {
            var @classes = await _classRepository.GetAllAsync();
            return View(@classes);
        }
    }
}
