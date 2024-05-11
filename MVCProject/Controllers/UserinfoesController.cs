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
    public class UserinfoesController : Controller
    {
        private readonly ModelContext _context;

        public UserinfoesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Userinfoes
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Userinfos.Include(u => u.Role);
            return View(await modelContext.ToListAsync());
        }

        // GET: Userinfoes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Userinfos == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfos
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            return View(userinfo);
        }

        // GET: Userinfoes/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            return View();
        }

        // POST: Userinfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Firstname,Lastname,Email,Address,Age,Phonenum,RoleId,ImagePath")] Userinfo userinfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userinfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userinfo.RoleId);
            return View(userinfo);
        }

        // GET: Userinfoes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Userinfos == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfos.FindAsync(id);
            if (userinfo == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName", userinfo.RoleId);
            return View(userinfo);
        }

        // POST: Userinfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("UserId,Firstname,Lastname,Email,Address,Age,Phonenum,RoleId,ImagePath")] Userinfo userinfo)
        {
            if (id != userinfo.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userinfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserinfoExists(userinfo.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", userinfo.RoleId);
            return View(userinfo);
        }

        // GET: Userinfoes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Userinfos == null)
            {
                return NotFound();
            }

            var userinfo = await _context.Userinfos
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userinfo == null)
            {
                return NotFound();
            }

            return View(userinfo);
        }

        // POST: Userinfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Userinfos == null)
            {
                return Problem("Entity set 'ModelContext.Userinfos'  is null.");
            }
            var userinfo = await _context.Userinfos.FindAsync(id);
            if (userinfo != null)
            {
                _context.Userinfos.Remove(userinfo);
            }
            
            await _context.SaveChangesAsync();
            TempData["message"] = "you Are successfully delete this user";
            return RedirectToAction(nameof(Index));
        }

        private bool UserinfoExists(decimal id)
        {
          return (_context.Userinfos?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
