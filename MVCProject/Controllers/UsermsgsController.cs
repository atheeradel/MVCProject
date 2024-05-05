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
    public class UsermsgsController : Controller
    {
        private readonly ModelContext _context;

        public UsermsgsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Usermsgs
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Usermsgs.Include(u => u.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Usermsgs/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Usermsgs == null)
            {
                return NotFound();
            }

            var usermsg = await _context.Usermsgs
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.MsgId == id);
            if (usermsg == null)
            {
                return NotFound();
            }

            return View(usermsg);
        }

        // GET: Usermsgs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Userinfos, "UserId", "UserId");
            return View();
        }

        // POST: Usermsgs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MsgId,UserId,Email,Username,Subject,Msg")] Usermsg usermsg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usermsg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Userinfos, "UserId", "UserId", usermsg.UserId);
            return View(usermsg);
        }

        // GET: Usermsgs/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Usermsgs == null)
            {
                return NotFound();
            }

            var usermsg = await _context.Usermsgs.FindAsync(id);
            if (usermsg == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Userinfos, "UserId", "UserId", usermsg.UserId);
            return View(usermsg);
        }

        // POST: Usermsgs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("MsgId,UserId,Email,Username,Subject,Msg")] Usermsg usermsg)
        {
            if (id != usermsg.MsgId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usermsg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsermsgExists(usermsg.MsgId))
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
            ViewData["UserId"] = new SelectList(_context.Userinfos, "UserId", "UserId", usermsg.UserId);
            return View(usermsg);
        }

        // GET: Usermsgs/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Usermsgs == null)
            {
                return NotFound();
            }

            var usermsg = await _context.Usermsgs
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.MsgId == id);
            if (usermsg == null)
            {
                return NotFound();
            }

            return View(usermsg);
        }

        // POST: Usermsgs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Usermsgs == null)
            {
                return Problem("Entity set 'ModelContext.Usermsgs'  is null.");
            }
            var usermsg = await _context.Usermsgs.FindAsync(id);
            if (usermsg != null)
            {
                _context.Usermsgs.Remove(usermsg);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsermsgExists(decimal id)
        {
          return _context.Usermsgs.Any(e => e.MsgId == id);
        }
    }
}
