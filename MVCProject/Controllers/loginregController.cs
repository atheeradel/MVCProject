using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;
using System.Runtime.InteropServices;

namespace MVCProject.Controllers
{
    public class loginregController : Controller
    {

        private readonly ModelContext _context;
        public loginregController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult login()
        {
            return View();
        }

        public IActionResult register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> login( Login userlogin)
        {
            var user = await _context.Logins.
                Where(x => x.Username == userlogin.Username && x.Password == userlogin.Password).SingleOrDefaultAsync();
            
            var user2= await _context.Userinfos.Where(x =>x.UserId==user.UserId).SingleOrDefaultAsync();
            if (user != null)
            {
                switch (user2.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetInt32("AdminId", (int)user.UserId);
                        return RedirectToAction("Index", "Admin");

                    case 2:
                        HttpContext.Session.SetInt32("ChefId", (int)user.UserId);
                        return RedirectToAction("ChefIndex", "Home");
                    case 3:
                        HttpContext.Session.SetInt32("UserId", (int)user.UserId);
                        return RedirectToAction("UserIndex", "Home");

                }
            }
            ModelState.AddModelError("", "UserName or Password are incorret");
            return View();
        }
        [HttpPost]
        public IActionResult register(Userinfo user,string role,string username,string password)
        {
            var u = _context.Logins.Where(x => x.Username == username).SingleOrDefault();
            if (u == null) { 
                if (ModelState.IsValid)
            {
                if (role == "2")
                {
                    user.RoleId = 2;
                }
                else if (role == "3")
                {
                    user.RoleId = 3;
                }
                _context.Add(user);
                _context.SaveChanges();
                Login log=new Login();
                log.Username= username;
                log.Password= password;
                log.UserId = user.UserId; 
                _context.Add(log);
                _context.SaveChanges();
                    
            }
            
            }
            ModelState.AddModelError("", "UserName already exist try again");
            TempData["message"] = "You Are Successfully Register to MasterChef";
            return RedirectToAction("login");
        }
    }
}
