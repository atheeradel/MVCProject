using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;

namespace MVCProject.Controllers

{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;

        public AdminController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("AdminId");

            var user = _context.Userinfos.Where(x => x.UserId == id).SingleOrDefault();
            return View(user);

            
        }
        public IActionResult Form()
        {
            return View();
        }

    }
}
