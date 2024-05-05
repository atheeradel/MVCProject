using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using System.Diagnostics;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
            

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserIndex()
        {
            var id = HttpContext.Session.GetInt32("UserId");

            var user = _context.Userinfos.Where(x => x.UserId == id).SingleOrDefault();
            return View(user);
        }
        public IActionResult ChefIndex()
        {
            var id = HttpContext.Session.GetInt32("ChefId");

            var user = _context.Userinfos.Where(x => x.UserId == id).SingleOrDefault();
            return View(user);
            
        }
        public IActionResult logout() {

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
           

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
