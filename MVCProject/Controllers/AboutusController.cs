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
    public class AboutusController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AboutusController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Aboutus
        public async Task<IActionResult> Index()
        {
              return _context.Aboutus != null ? 
                          View(await _context.Aboutus.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Aboutus'  is null.");
        }

        // GET: Aboutus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Aboutus == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .FirstOrDefaultAsync(m => m.AboutId == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // GET: Aboutus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aboutus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AboutId,ImageFile,Aboutusdescription,Trustedinfo,Profinfo,Expertinfo")] Aboutu aboutu)
        {
            if (ModelState.IsValid)
            {
                if (aboutu.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath;
                    string imageName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
                    string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        aboutu.ImageFile.CopyToAsync(fileStream);
                    }
                    aboutu.Aboutusmainimg = imageName;
                }
                _context.Add(aboutu);
                await _context.SaveChangesAsync();
                TempData["message"] = "you are successfuly created the record";
                return RedirectToAction(nameof(Index));
            }
            return View(aboutu);
        }

        // GET: Aboutus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Aboutus == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus.FindAsync(id);
            if (aboutu == null)
            {
                return NotFound();
            }
            return View(aboutu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("AboutId,ImageFile,Aboutusdescription,Trustedinfo,Profinfo,Expertinfo")] Aboutu aboutu)
        {
            if (id != aboutu.AboutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (aboutu.ImageFile != null)
                    {
                        // Check if the file is an image
                        var supportedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp" };
                        var mimeType = aboutu.ImageFile.ContentType;

                        if (!supportedTypes.Contains(mimeType))
                        {
                            TempData["message"] = "please only upload image file only try again!";
                            return View(aboutu);
                        }

                        string wwwrootPath = _webHostEnvironment.WebRootPath;
                        string imageName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
                        string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);

                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await aboutu.ImageFile.CopyToAsync(fileStream);
                        }
                        aboutu.Aboutusmainimg = imageName;
                    }

                    _context.Update(aboutu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AboutuExists(aboutu.AboutId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["message"] = "You have successfully updated the record.";
                return RedirectToAction(nameof(Index));
            }
            return View(aboutu);
        }








        // POST: Aboutus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(decimal id, [Bind("AboutId,ImageFile,Aboutusdescription,Trustedinfo,Profinfo,Expertinfo")] Aboutu aboutu)
        //{
        //    if (id != aboutu.AboutId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (aboutu.ImageFile != null)
        //            {
        //                string wwwrootPath = _webHostEnvironment.WebRootPath;
        //                string imageName = Guid.NewGuid().ToString() + "_" + aboutu.ImageFile.FileName;
        //                string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);
        //                using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //                {
        //                    aboutu.ImageFile.CopyToAsync(fileStream);
        //                }
        //                aboutu.Aboutusmainimg = imageName;
        //            }
        //                _context.Update(aboutu);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AboutuExists(aboutu.AboutId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        TempData["message"] = "you are successfuly updated the record";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(aboutu);
        //}

        // GET: Aboutus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Aboutus == null)
            {
                return NotFound();
            }

            var aboutu = await _context.Aboutus
                .FirstOrDefaultAsync(m => m.AboutId == id);
            if (aboutu == null)
            {
                return NotFound();
            }

            return View(aboutu);
        }

        // POST: Aboutus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Aboutus == null)
            {
                return Problem("Entity set 'ModelContext.Aboutus'  is null.");
            }
            var aboutu = await _context.Aboutus.FindAsync(id);
            if (aboutu != null)
            {
                _context.Aboutus.Remove(aboutu);
            }
            
            await _context.SaveChangesAsync();
            TempData["message"] = "you are successfuly deleted the record";
            return RedirectToAction(nameof(Index));
        }

        private bool AboutuExists(decimal id)
        {
          return (_context.Aboutus?.Any(e => e.AboutId == id)).GetValueOrDefault();
        }
    }
}
