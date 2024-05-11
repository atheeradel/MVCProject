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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;

        }

        

        public IActionResult Aboutus()
        {
            ViewBag.aboutus=_context.Aboutus.ToList();
            return View();
        }
        public IActionResult aboutususer()
        {
            ViewBag.aboutus = _context.Aboutus.ToList();
            return View();
        }

        public IActionResult aboutuschef()
        {
            ViewBag.aboutus = _context.Aboutus.ToList();
            return View();
        }
        public IActionResult contactus()
        {
            ViewBag.con = _context.Userinfos.Where(x => x.RoleId == 1).ToList();
            ViewBag.ad = _context.Contactus.ToList();
            return View();
        }
        public IActionResult contactusChef()
        {
            ViewBag.Con = _context.Userinfos.Where(x => x.RoleId == 1).ToList();
            ViewBag.ad = _context.Contactus.ToList();
            return View();
        }

        public IActionResult contactusGust()
        {
            ViewBag.Con = _context.Userinfos.Where(x => x.RoleId == 1).ToList();
            ViewBag.ad = _context.Contactus.ToList();
            return View();
        }
        public IActionResult Index()
        {
            ViewBag.category = _context.Categories.ToList();
            ViewBag.chef = _context.Userinfos.Where(x => x.RoleId == 2 && x.ImagePath != null).ToList();
            ViewBag.recipe = _context.Recipes.Where(x => x.Status == 1).ToList();
            return View();
        }

        public IActionResult getrecipebycategory(int id)
        {  
            var rec=_context.Recipes.Where(x=>x.CatId == id && x.Status==1).ToList();
            ViewBag.category = _context.Categories.ToList();
            return View(rec);
        }

        public IActionResult getrecipebychef(int id)
        {
            var rec = _context.Recipes.Where(x => x.UserId == id && x.Status == 1).ToList();
            ViewBag.category = _context.Categories.ToList();
            return View(rec);
        }
        public IActionResult ViewRecipe(int id)
        {
            var rec = _context.Recipes.Where(x => x.RecId == id).SingleOrDefault();
            ViewBag.recipe = _context.Recipes.Where(x => x.Status == 1).ToList();
            return View(rec);
        }
        public IActionResult ViewRecipes(int id)
        {
            var rec = _context.Recipes.Where(x => x.RecId == id).SingleOrDefault();
            ViewBag.recipe = _context.Recipes.Where(x => x.Status == 1).ToList();
            return View(rec);
        }


        public IActionResult getrecipebycheff(int id)
        {
            var rec = _context.Recipes.Where(x => x.UserId == id && x.Status == 1).ToList();
            ViewBag.category = _context.Categories.ToList();
            return View(rec);
        }
        public IActionResult getrecipebycat(int id)
        {
            var rec = _context.Recipes.Where(x => x.CatId == id && x.Status == 1).ToList();
            ViewBag.category = _context.Categories.ToList();

            return View(rec);
        }
        public IActionResult UserIndex()
        {
            var id = HttpContext.Session.GetInt32("UserId");

            var user = _context.Userinfos.Where(x => x.UserId == id).SingleOrDefault();
            ViewBag.category = _context.Categories.ToList();
            ViewBag.chef=_context.Userinfos.Where(x=>x.RoleId==2 && x.ImagePath !=null).ToList();
            ViewBag.recipe=_context.Recipes.Where(x=>x.Status==1).ToList();

            return View(user);
        }
        public IActionResult ChefIndex()
        {
            var id = HttpContext.Session.GetInt32("ChefId");

            var user = _context.Userinfos.Where(x => x.UserId == id).SingleOrDefault();
            ViewBag.category = _context.Categories.ToList();
            ViewBag.chef = _context.Userinfos.Where(x => x.RoleId == 2 && x.ImagePath != null).ToList();
            ViewBag.recipe = _context.Recipes.Where(x => x.Status == 1).ToList();
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
        [HttpGet]
        public IActionResult EditProfile(int id) {
            var user=_context.Userinfos.Where(x=>x.UserId==id).SingleOrDefault();
            return View(user);
        }
        public IActionResult EditUser(int id)
        {
            var user = _context.Userinfos.Where(x => x.UserId == id).SingleOrDefault();
            return View(user);
        }



        [HttpPost]
        public IActionResult contactus(string Username,string Email,string Subject,string Msg)
        {
            Usermsg msg = new Usermsg();
            var userid = HttpContext.Session.GetInt32("UserId");
            msg.UserId = userid;
            msg.Username = Username;
            msg.Email = Email;
            msg.Subject= Subject;
            msg.Msg= Msg;
            _context.Add(msg);
            _context.SaveChanges();
            TempData["message"] = "Your Message Successfully Send";
            return  View();
        }
        [HttpPost]
        public IActionResult contactusChef(string Username, string Email, string Subject, string Msg)
        {
            Usermsg msg = new Usermsg();
            var userid = HttpContext.Session.GetInt32("ChefId");
            msg.UserId = userid;
            msg.Username = Username;
            msg.Email = Email;
            msg.Subject = Subject;
            msg.Msg = Msg;
            _context.Add(msg);
            _context.SaveChanges();
            TempData["message"] = "Your Message Successfully Send";
            return View();
        }

        [HttpPost]
        public IActionResult contactusGust(string Username, string Email, string Subject, string Msg)
        {
            Usermsg msg = new Usermsg();
            
            msg.Username = Username;
            msg.Email = Email;
            msg.Subject = Subject;
            msg.Msg = Msg;
            _context.Add(msg);
            _context.SaveChanges();
            TempData["message"] = "Your Message Successfully Send";
            return View();
        }
        public IActionResult Addtest()
        {
            return View();
        }

       [HttpPost]
        public IActionResult Addtest(string Msg)
        {
            Testimonal test = new Testimonal();
            var userid = HttpContext.Session.GetInt32("UserId");
            test.UserId = userid;
            test.Msg = Msg;
            test.DateAdd= DateTime.Now;
            test.Status = 2;
            _context.Add(test);
            _context.SaveChanges();
            TempData["message"] = "Your Testimonal Successfully Send";
            return View();
        }


        public IActionResult testimonal()
        {
            
            var test = _context.Testimonals.Where(x => x.Status == 1).Include(t => t.User).ToList();
            return View(test);
        }

        public IActionResult testChef()
        {
            var test = _context.Testimonals.Where(x => x.Status == 1).Include(t => t.User).ToList();
            return View(test);
        }

        public IActionResult testGust()
        {
            var test = _context.Testimonals.Where(x => x.Status == 1).Include(t => t.User).ToList();
            return View(test);
        }







        [HttpPost]
        public async Task<IActionResult> EditProfile(int id,[Bind("UserId", "Firstname", "Lastname", "Email", "Address", "Age", "Phonenum", "ImageFile")]Userinfo user)
        {

            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.ImageFile != null)
                    {
                        string wwwrootPath = _webHostEnvironment.WebRootPath;
                        string imageName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                        string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            user.ImageFile.CopyToAsync(fileStream);
                        }
                        user.ImagePath = imageName;
                        user.RoleId = 2;

                    }
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

        [HttpPost]
        public async Task<IActionResult> EditUser(int id, [Bind("UserId", "Firstname", "Lastname", "Email", "Address", "Age", "Phonenum", "ImageFile")] Userinfo user)
        {

            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (user.ImageFile != null)
                    {
                        string wwwrootPath = _webHostEnvironment.WebRootPath;
                        string imageName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                        string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            user.ImageFile.CopyToAsync(fileStream);
                        }
                        user.ImagePath = imageName;
                        user.RoleId = 3;

                    }
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
                return RedirectToAction(nameof(UserProfile));
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
