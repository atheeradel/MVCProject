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
    public class UserrecipesController : Controller
    {
        private readonly ModelContext _context;

        public UserrecipesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Userrecipes
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Userrecipes.Include(u => u.Rec).Include(u => u.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Userrecipes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Userrecipes == null)
            {
                return NotFound();
            }

            var userrecipe = await _context.Userrecipes
                .Include(u => u.Rec)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ReqId == id);
            if (userrecipe == null)
            {
                return NotFound();
            }

            return View(userrecipe);
        }

        // GET: Userrecipes/Create
        public IActionResult Create()
        {
            ViewData["RecId"] = new SelectList(_context.Recipes, "RecId", "Name");
            ViewData["UserId"] = new SelectList(_context.Userinfos, "UserId", "Firstname");
            return View();
        }

        // POST: Userrecipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReqId,RecId,UserId,ReqDate,Status")] Userrecipe userrecipe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userrecipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RecId"] = new SelectList(_context.Recipes, "RecId", "RecId", userrecipe.RecId);
            ViewData["UserId"] = new SelectList(_context.Userinfos, "UserId", "UserId", userrecipe.UserId);
            return View(userrecipe);
        }

        // GET: Userrecipes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Userrecipes == null)
            {
                return NotFound();
            }

            var userrecipe = await _context.Userrecipes.FindAsync(id);
            if (userrecipe == null)
            {
                return NotFound();
            }
            ViewData["RecId"] = new SelectList(_context.Recipes, "RecId", "RecId", userrecipe.RecId);
            ViewData["UserId"] = new SelectList(_context.Userinfos, "UserId", "UserId", userrecipe.UserId);
            return View(userrecipe);
        }

        // POST: Userrecipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("ReqId,RecId,UserId,ReqDate,Status")] Userrecipe userrecipe)
        {
            if (id != userrecipe.ReqId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userrecipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserrecipeExists(userrecipe.ReqId))
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
            ViewData["RecId"] = new SelectList(_context.Recipes, "RecId", "RecId", userrecipe.RecId);
            ViewData["UserId"] = new SelectList(_context.Userinfos, "UserId", "UserId", userrecipe.UserId);
            return View(userrecipe);
        }

        // GET: Userrecipes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Userrecipes == null)
            {
                return NotFound();
            }

            var userrecipe = await _context.Userrecipes
                .Include(u => u.Rec)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.ReqId == id);
            if (userrecipe == null)
            {
                return NotFound();
            }

            return View(userrecipe);
        }

        // POST: Userrecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Userrecipes == null)
            {
                return Problem("Entity set 'ModelContext.Userrecipes'  is null.");
            }
            var userrecipe = await _context.Userrecipes.FindAsync(id);
            if (userrecipe != null)
            {
                _context.Userrecipes.Remove(userrecipe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserrecipeExists(decimal id)
        {
          return (_context.Userrecipes?.Any(e => e.ReqId == id)).GetValueOrDefault();
        }
    }
}
