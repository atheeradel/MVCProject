using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult UserProfile()
        {
            var userid = HttpContext.Session.GetInt32("UserId");

            var user = _context.Userinfos.Where(x => x.UserId == userid).SingleOrDefault();
            return View(user);
        }
        public IActionResult ChefProfile()
        {
            var id = HttpContext.Session.GetInt32("ChefId");

            var user = _context.Userinfos.Where(x => x.UserId == id).SingleOrDefault();
            if (user.ImagePath == null)
            {
                TempData["message"] = "you must upload your profile image";
            }
            return View(user);

        }
        public IActionResult EditProfile(int id) {
            var user=_context.Userinfos.Where(x=>x.UserId==id).SingleOrDefault();
            return View(user);
        }

        public async Task<IActionResult> EditProfile(int id,[Bind("UserId", "Firstname", "Lastname", "Email", "Address", "Age", "Phonenum")]Userinfo user)
        {

            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserinfoExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ChefProfile));
            }
            return View(user);
        }
      

        private bool UserinfoExists(decimal userId)
        {
            throw new NotImplementedException();
        }

        



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
