using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MVCProject.Models;
using System.Net.Mail;



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

        public IActionResult checkout(int id )
        {
            var rec = _context.Recipes.Where(x => x.RecId == id).SingleOrDefault();
            return View(rec);
        }
        [HttpPost]
        public IActionResult checkout(string nameoncard, string number, DateTime date, string cvv,int id)
        {
            var v = _context.Visas.ToList();
            var r = _context.Recipes.Where(x => x.RecId == id).SingleOrDefault();
            var price = r.Price;
            foreach (var item in v)
            {
                if(item.Cardname==nameoncard && item.Cardnum==number  && item.Cvc == cvv && item.Amountofmoney> price && item.Expiredate>=date)
                {
                    
                    Userrecipe rec = new Userrecipe();
                    var userid = HttpContext.Session.GetInt32("UserId");
                    item.Amountofmoney = item.Amountofmoney - price;
                    rec.UserId = userid;
                    rec.RecId = id;
                    rec.ReqDate = DateTime.Now;
                    _context.Add(rec);
                    _context.SaveChanges();
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        iTextSharp.text.Document doc = new iTextSharp.text.Document();
                        Font font;
                        PdfWriter writer = PdfWriter.GetInstance(doc, memoryStream);
                        doc.Open();
                        doc.SetPageSize(PageSize.A4);
                        doc.SetMargins(10f, 10f, 25f, 10f);

                        Paragraph title = new Paragraph("Master Chef Recipe");
                        font = FontFactory.GetFont("Tahoma", 8f, 1);
                        string path = _webHostEnvironment.WebRootPath + "/Home/images";
                        string path2 = _webHostEnvironment.WebRootPath + "/Images";

                        string imgcombine = Path.Combine(path, "Recipe-removebg-preview.png");
                        string imgcombine2 = Path.Combine(path2, $"{r.Image}");
                        Image img = Image.GetInstance(imgcombine);
                        Image img2 = Image.GetInstance(imgcombine2);
                        img.ScaleToFit(300f, img.Height);

                        img.BorderColor = BaseColor.Black;
                        img.BorderWidth = 2f;
                        img2.ScaleToFit(200f, img.Height);
                        img2.BorderWidth = 1f;
                        doc.Add(img);

                        // Add content to PDF
                        doc.Add(new Paragraph($"Welcome To Master Chef our customer  Thank you for your trust in our recipes 🙏"));
                        doc.Add(new Paragraph($"   "));
                        doc.Add(new Paragraph($"Your Recipe Name 🍽️: {r.Name}"));
                        doc.Add(new Paragraph($"   "));
                        doc.Add(new Paragraph($"Your Recipe Ingredients: {r.Ingrediants}"));
                        doc.Add(new Paragraph($"   "));
                        doc.Add(new Paragraph($"Your Recipe Instructions: {r.Instruction}"));
                        doc.Add(img2);
                        doc.Add(new Paragraph($"   "));
                        doc.Add(new Paragraph($" Dont Forget to share us your opinon for our recipes 😉  "));
                        // Add other properties as needed

                        doc.Close();

                        // Store PDF in wwwroot
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string pdfPath = Path.Combine(wwwRootPath, "pdfs", "generated.pdf");

                        using (var fileStream = new FileStream(pdfPath, FileMode.Create))
                        {
                            memoryStream.WriteTo(fileStream);
                        }
                    }
                    var record = _context.Userinfos.Where(x => x.UserId == userid).SingleOrDefault();
                   
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.ethereal.email");
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("liliane79@ethereal.email", "U9Qmf8sPcwKFhHXfVP");
                    SmtpServer.EnableSsl = true;
                    //MailAddress from = new MailAddress("MasterChef@gamil.com");
                    mail.From = new MailAddress("liliane79@ethereal.email");
                    mail.To.Add($"{record.Email}");
                    mail.Body = $"Thank You {record.Firstname} {record.Lastname} for your Purchase  your invoice is ${r.Price} and your recipe is by this attached Pdf File"; ;
                    Attachment attachment = new Attachment("D:\\dirsttask\\MVCProject - Copy\\MVCProject\\wwwroot\\pdfs\\generated.pdf");
                    mail.Attachments.Add(attachment);
                    SmtpServer.Send(mail);
                    TempData["message"] = "you are successfully checkout , Check your email to see your Recipe ";
                    
                    
                     

                    break;
                }
            }
            return View(r);
        }

        public IActionResult userorder()
        {
            var id = HttpContext.Session.GetInt32("UserId");
            
			var modelContext = _context.Userrecipes.Where(x => x.UserId == id).Include(u => u.Rec).Include(u => u.User);
			return View( modelContext.ToList());

			
        }

        public IActionResult customerreq()
        {
            var chef = HttpContext.Session.GetInt32("ChefId");
            var rec = _context.Recipes.Where(x => x.UserId == chef).ToList();
            var modelContext = _context.Userrecipes.Where(x=>x.Rec.UserId==chef).Include(u => u.Rec).Include(u => u.User).ToList();
            
           
            return View(modelContext);

            
        }


        //public IActionResult Discount(string code,int id)
        //{
        //    if(code== "offT30")
        //    {
        //        var rec= _context.Recipes.Where(x => x.RecId == id).SingleOrDefault();
        //        ViewBag.discount = ((int)rec.Price) - (0.10* (int)rec.Price); 
        //    }
        //    return RedirectToAction("checkout(id)");
        //}



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

        public IActionResult searchrecipe()
        {
            var rec = _context.Recipes.Where(x => x.Status == 1).ToList();
            ViewBag.category = _context.Categories.ToList();
            return View(rec);
        }

        [HttpPost]
        public IActionResult searchrecipe(string word)
        {
            var rec = _context.Recipes.Where(x => x.Status == 1).ToList();
            ViewBag.category = _context.Categories.ToList();
            word = word.ToLower();
            if (String.IsNullOrEmpty(word))
            {
                return View(rec);
            }
            else
            {
                rec = rec.Where(s => s.Name.ToLower().Contains(word)).ToList();
                return View(rec);
            }



        }

        public IActionResult searchrecipechef()
        {
            var rec = _context.Recipes.Where(x => x.Status == 1).ToList();
            ViewBag.category = _context.Categories.ToList();
            return View(rec);
        }

        [HttpPost]
        public IActionResult searchrecipechef(string word)
        {
            var rec = _context.Recipes.Where(x => x.Status == 1).ToList();
            ViewBag.category = _context.Categories.ToList();
            word = word.ToLower();
            if (String.IsNullOrEmpty(word))
            {
                return View(rec);
            }
            else
            {
                rec = rec.Where(s => s.Name.ToLower().Contains(word)).ToList();
                return View(rec);
            }


        }

        public IActionResult searchrecipegust()
        {
            var rec = _context.Recipes.Where(x => x.Status == 1).ToList();
            ViewBag.category = _context.Categories.ToList();
            return View(rec);
        }

        [HttpPost]
        public IActionResult searchrecipegust(string word)
        {
            var rec = _context.Recipes.Where(x => x.Status == 1).ToList();
            ViewBag.category = _context.Categories.ToList();
            word=word.ToLower();
            if (String.IsNullOrEmpty(word))
            {
                return View(rec);
            }
            else
            {
                rec = rec.Where(s => s.Name.ToLower().Contains(word)).ToList();
                return View(rec);
            }


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
