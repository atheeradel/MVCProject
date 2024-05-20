using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Reflection.Metadata;
namespace MVCProject.Controllers

{
    public class AdminController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult recipestatus(int id) {
            var rec = _context.Recipes.Where(x => x.RecId == id).SingleOrDefault();

              rec.Status = 1;
            _context.Update(rec);
             _context.SaveChanges();

            TempData["message"] = "You Are Successfully Accept the recipe of the chef ";
            return RedirectToAction("recipeIndex");
        }
        public IActionResult teststatus(int id)
        {
            var test = _context.Testimonals.Where(x => x.TestId == id).SingleOrDefault();

            test.Status = 1;
            _context.Update(test);
            _context.SaveChanges();

            TempData["message"] = "You Are Successfully Accept the testimonal of the User ";
            return RedirectToAction("test");
        }

        public IActionResult EditAdmin(int id)
        {
            var user = _context.Userinfos.Where(x => x.UserId == id).SingleOrDefault();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> EditAdmin(int id, [Bind("UserId", "Firstname", "Lastname", "Email", "Address", "Age", "Phonenum", "ImageFile")] Userinfo user)
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
                        user.RoleId = 1;

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
                return RedirectToAction(nameof(Adminprofile));
            }
            return View(user);
        }

        private bool UserinfoExists(decimal userId)
        {
            throw new NotImplementedException();
        }

        public IActionResult msgbyuser()
        {
            var modelContext = _context.Usermsgs.ToList();
            return View( modelContext);

        }
        public IActionResult rejectstatus(int id)
        {
            var rec = _context.Recipes.Where(x => x.RecId == id).SingleOrDefault();

            rec.Status = 0;
            _context.Update(rec);
            _context.SaveChanges();

            TempData["message"] = "You Are Successfully Reject the recipe of the chef ";
            return RedirectToAction("recipeIndex");
        }
        public IActionResult rejecttet(int id)
        {
            var test = _context.Testimonals.Where(x => x.TestId == id).SingleOrDefault();

            test.Status = 0;
            _context.Update(test);
            _context.SaveChanges();

            TempData["message"] = "You Are Successfully Reject the testimonal of the User ";
            return RedirectToAction("test");
        }

        public IActionResult recipeIndex()
        {
            var modelContext = _context.Recipes.Include(r => r.Cat).Include(r => r.User).ToList();
            ViewBag.TotalPrice = modelContext.Sum(x => x.Price);
            return View( modelContext);
        }

        [HttpPost]
        public IActionResult recipeIndex(DateTime? startDate, DateTime? endDate)
        {
            var result = _context.Recipes.Include(r => r.Cat).Include(r => r.User).ToList();

            if (startDate == null && endDate == null)
            {
                //ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);
                return View(result);
            }
            else if (startDate != null && endDate == null)
            {

                result = result.Where(x => x.Dateadd.Value.Date >= startDate).ToList();
                //ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);

                return View(result);
            }
            else if (startDate == null && endDate != null)
            {

                result = result.Where(x => x.Dateadd.Value.Date <= endDate).ToList();
                //ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);

                return View(result);
            }
            else
            {

                result = result.Where(x => x.Dateadd.Value.Date >= startDate && x.Dateadd.Value.Date <= endDate).ToList();
                //ViewBag.TotalPrice = result.Sum(x => x.Product.Price * x.Quantity);
                return View(result);
            }
        }

       
        public IActionResult reports()
        {
            var modelContext = _context.Userrecipes.Include(u => u.Rec).Include(u => u.User);
            ViewBag.TotalPrice = modelContext.Sum(x => x.Rec.Price);
            ViewBag.Totalsales =  modelContext.Sum(x => (int)x.Rec.Price)*0.1;
            ViewBag.netprofit =  10- ViewBag.Totalsales ;
            return View( modelContext.ToList());
        }
        [HttpPost]
        public IActionResult reports(string year, DateTime? month)
        {
            var modelContext = _context.Userrecipes.Include(u => u.Rec).Include(u => u.User).ToList();

            int parsedYear;
            bool isYearParsed = int.TryParse(year, out parsedYear);

            // Filter by year if provided
            if (isYearParsed)
            {
                modelContext = modelContext.Where(x => x.ReqDate.HasValue && x.ReqDate.Value.Year == parsedYear).ToList();
                ViewBag.TotalPrice = modelContext.Sum(x => x.Rec.Price);
                ViewBag.Totalsales = modelContext.Sum(x => (int)x.Rec.Price) * 0.1;
                ViewBag.netprofit = 10 - ViewBag.Totalsales;
                return View(modelContext);
            }

            // Filter by month if provided
            if (month.HasValue)
            {
                modelContext = modelContext.Where(x => x.ReqDate.HasValue && x.ReqDate.Value.Year == month.Value.Year && x.ReqDate.Value.Month == month.Value.Month).ToList();
                ViewBag.TotalPrice = modelContext.Sum(x => x.Rec.Price);
                ViewBag.Totalsales = modelContext.Sum(x => (int)x.Rec.Price) * 0.1;
                ViewBag.netprofit = 10 - ViewBag.Totalsales;
                return View(modelContext);
            }

            // Calculate and set ViewBag properties
            else
            {
                ViewBag.TotalPrice = modelContext.Sum(x => x.Rec.Price);
                ViewBag.Totalsales = modelContext.Sum(x => (int)x.Rec.Price) * 0.1;
                ViewBag.netprofit = 10 - ViewBag.Totalsales;

                // Return the filtered data to the view
                return View(modelContext);
            }
            
        }

        public IActionResult changeadmin(int id)
        {
            var user = _context.Logins.Where(x => x.UserId == id).SingleOrDefault();
            return View(user);
        }

        [HttpPost]
        public IActionResult changeadmin(int id, string Username, string pass)
        {
            var userid = HttpContext.Session.GetInt32("AdminId");
            var user = _context.Logins.Where(x => x.UserId == userid).SingleOrDefault();


            user.Username = Username;
            user.Password = pass;
            user.UserId = userid;
            user.LogId = id;
            _context.Update(user);
            _context.SaveChanges();
            TempData["message"] = "You Are Successfully Changed your password or UserName  ";
            return RedirectToAction("Adminprofile");
        }









        public IActionResult test()
        {
            var modelContext = _context.Testimonals.Include(t => t.User).ToList();
            return View(modelContext);
        }


    
        public IActionResult Index()

        {

            ViewBag.chefnum = _context.Userinfos.Count(x => x.RoleId == 2);
            ViewBag.usernum = _context.Userinfos.Count(x => x.RoleId == 3);
            ViewBag.recipenum = _context.Recipes.Count();
            ViewBag.recipenumaccept = _context.Recipes.Count(x => x.Status==1);
            ViewBag.recipenumreject= _context.Recipes.Count(x => x.Status == 0);
            ViewBag.order = _context.Userrecipes.Count();
            var id = HttpContext.Session.GetInt32("AdminId");

            var user = _context.Userinfos.Where(x => x.UserId == id).SingleOrDefault();
            
            var salesData = _context.Userrecipes.Include(u => u.Rec).Include(u => u.User).ToList();
            ViewBag.recipe = _context.Recipes.Where(x => x.Status == 1).Include(r => r.Cat).Include(r => r.User).ToList();
            //// Process data to calculate total sales and profit
            ViewBag.totalSales = salesData.Sum(x => x.Rec.Price);
            ViewBag.cat = _context.Categories.ToList();
            ViewBag.totalCost =  salesData.Sum(x => (int)x.Rec.Price) *0.1;
            ViewBag.profit = 10- ViewBag.totalCost ;

            return View(user);

            
        }
        public IActionResult Adminprofile()
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
