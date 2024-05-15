using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class WishlistsController : Controller
    {
        private readonly ModelContext _context;

        public WishlistsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Wishlists
        public IActionResult Index(int id)
        {
            var user = HttpContext.Session.GetInt32("UserId");
            var modelContext = _context.Wishlists.Include(w => w.Rec).Include(w => w.User).Where(x=>x.User.UserId==user).ToList();
            return View( modelContext);
        }

        // GET: Wishlists/Details/5
       

        // GET: Wishlists/Create
       

        // POST: Wishlists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id)
        {
            var user = HttpContext.Session.GetInt32("UserId");
            Wishlist list = new Wishlist();
            list.UserId = user;
            list.RecId= id; 

            _context.Add(list);
            _context.SaveChanges();
             return RedirectToAction();
            
                   }

        // GET: Wishlists/Edit/5
      

        // POST: Wishlists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       

        // GET: Wishlists/Delete/5
       
        // POST: Wishlists/Delete/5
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Wishlists == null)
            {
                return Problem("Entity set 'ModelContext.Wishlists'  is null.");
            }
            var wishlist = await _context.Wishlists.FindAsync(id);
            if (wishlist != null)
            {
                _context.Wishlists.Remove(wishlist);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WishlistExists(decimal id)
        {
          return (_context.Wishlists?.Any(e => e.WishId == id)).GetValueOrDefault();
        }
    }
}
