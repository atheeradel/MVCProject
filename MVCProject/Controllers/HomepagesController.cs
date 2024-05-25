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
    public class HomepagesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomepagesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;   
        }

        // GET: Homepages
        public async Task<IActionResult> Index()
        {
              return _context.Homepages != null ? 
                          View(await _context.Homepages.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Homepages'  is null.");
        }

        // GET: Homepages/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Homepages == null)
            {
                return NotFound();
            }

            var homepage = await _context.Homepages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (homepage == null)
            {
                return NotFound();
            }

            return View(homepage);
        }

        // GET: Homepages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Homepages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PageId,Phonenum,ImageFile,Bannerdescription")] Homepage homepage)
        {
            if (ModelState.IsValid)
            {
                if (homepage.ImageFile != null)
                {
                    // Check if the file is an image
                    var supportedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp" };
                    var fileExtension = Path.GetExtension(homepage.ImageFile.FileName).ToLower();
                    var mimeType = homepage.ImageFile.ContentType;

                    if (!supportedTypes.Contains(mimeType))
                    {
                        TempData["message"] = "please Upload only image type try again.";
                        return View(homepage);
                    }

                    string wwwrootPath = _webHostEnvironment.WebRootPath;
                    string imageName = Guid.NewGuid().ToString() + "_" + homepage.ImageFile.FileName;
                    string fullPath = Path.Combine(wwwrootPath + "/img/", imageName);

                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await homepage.ImageFile.CopyToAsync(fileStream);
                    }
                    homepage.Imglogo = imageName;

                    _context.Add(homepage);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "You have successfully created the record.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("ImageFile", "Please upload an image file.");
                }
            }
            return View(homepage);
        }



        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("PageId,Phonenum,ImageFile,Bannerdescription")] Homepage homepage)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        if (homepage.ImageFile != null)
        //        {
        //            string wwwrootPath = _webHostEnvironment.WebRootPath;
        //            string imageName = Guid.NewGuid().ToString() + "_" + homepage.ImageFile.FileName;
        //            string fullPath = Path.Combine(wwwrootPath + "/img/", imageName);

        //            using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                await homepage.ImageFile.CopyToAsync(fileStream);
        //            }
        //            homepage.Imglogo = imageName;
        //        }

        //        _context.Add(homepage);
        //        await _context.SaveChangesAsync();
        //        TempData["message"] = "you are successfuly created the record";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(homepage);
        //}

        // GET: Homepages/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Homepages == null)
            {
                return NotFound();
            }

            var homepage = await _context.Homepages.FindAsync(id);
            if (homepage == null)
            {
                return NotFound();
            }
            return View(homepage);
        }

        // POST: Homepages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("PageId,Phonenum,ImageFile,Bannerdescription")] Homepage homepage)
        {
            if (id != homepage.PageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (homepage.ImageFile != null)
                    {
                        // Check if the file is an image
                        var supportedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp" };
                        var mimeType = homepage.ImageFile.ContentType;

                        if (!supportedTypes.Contains(mimeType))
                        {
                            TempData["message"] = "please Upload only image type try again.";
                            return View(homepage);
                        }

                        string wwwrootPath = _webHostEnvironment.WebRootPath;
                        string imageName = Guid.NewGuid().ToString() + "_" + homepage.ImageFile.FileName;
                        string fullPath = Path.Combine(wwwrootPath + "/img/", imageName);
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            await homepage.ImageFile.CopyToAsync(fileStream);
                        }
                        homepage.Imglogo = imageName;
                    }

                    _context.Update(homepage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomepageExists(homepage.PageId))
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
            return View(homepage);
        }
        //public async Task<IActionResult> Edit(decimal id, [Bind("PageId,Phonenum,ImageFile,Bannerdescription")] Homepage homepage)
        //{
        //    if (id != homepage.PageId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (homepage.ImageFile != null)
        //            {
        //                string wwwrootPath = _webHostEnvironment.WebRootPath;
        //                string imageName = Guid.NewGuid().ToString() + "_" + homepage.ImageFile.FileName;
        //                string fullPath = Path.Combine(wwwrootPath + "/img/", imageName);
        //                using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //                {
        //                    homepage.ImageFile.CopyToAsync(fileStream);
        //                }
        //                homepage.Imglogo = imageName;
        //            }
        //            //if (homepage.ImageFile1 != null)
        //            //{
        //            //    string wwwrootPath = _webHostEnvironment.WebRootPath;
        //            //    string imageName = Guid.NewGuid().ToString() + "_" + homepage.ImageFile1.FileName;
        //            //    string fullPath = Path.Combine(wwwrootPath + "/img/", imageName);
        //            //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //            //    {
        //            //        homepage.ImageFile1.CopyToAsync(fileStream);
        //            //    }
        //            //    homepage.Imgbannerone = imageName;
        //            //}
        //            //if (homepage.ImageFile2 != null)
        //            //{
        //            //    string wwwrootPath = _webHostEnvironment.WebRootPath;
        //            //    string imageName = Guid.NewGuid().ToString() + "_" + homepage.ImageFile2.FileName;
        //            //    string fullPath = Path.Combine(wwwrootPath + "/img/", imageName);
        //            //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //            //    {
        //            //        homepage.ImageFile2.CopyToAsync(fileStream);
        //            //    }
        //            //    homepage.Imgbannertwo = imageName;
        //            //}
        //            //if (homepage.ImageFile3 != null)
        //            //{
        //            //    string wwwrootPath = _webHostEnvironment.WebRootPath;
        //            //    string imageName = Guid.NewGuid().ToString() + "_" + homepage.ImageFile3.FileName;
        //            //    string fullPath = Path.Combine(wwwrootPath + "/img/", imageName);
        //            //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //            //    {
        //            //        homepage.ImageFile3.CopyToAsync(fileStream);
        //            //    }
        //            //    homepage.Imgbannerthree = imageName;
        //            //}
        //            //if (homepage.ImageFile4 != null)
        //            //{
        //            //    string wwwrootPath = _webHostEnvironment.WebRootPath;
        //            //    string imageName = Guid.NewGuid().ToString() + "_" + homepage.ImageFile4.FileName;
        //            //    string fullPath = Path.Combine(wwwrootPath + "/img/", imageName);
        //            //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //            //    {
        //            //        homepage.ImageFile4.CopyToAsync(fileStream);
        //            //    }
        //            //    homepage.Imgonedesign = imageName;
        //            //}
        //            //if (homepage.ImageFile5 != null)
        //            //{
        //            //    string wwwrootPath = _webHostEnvironment.WebRootPath;
        //            //    string imageName = Guid.NewGuid().ToString() + "_" + homepage.ImageFile5.FileName;
        //            //    string fullPath = Path.Combine(wwwrootPath + "/img/", imageName);
        //            //    using (var fileStream = new FileStream(fullPath, FileMode.Create))
        //            //    {
        //            //        homepage.ImageFile5.CopyToAsync(fileStream);
        //            //    }
        //            //    homepage.Imgtwodesign = imageName;
        //            //}










        //            _context.Update(homepage);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!HomepageExists(homepage.PageId))
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
        //    return View(homepage);
        //}

        // GET: Homepages/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Homepages == null)
            {
                return NotFound();
            }

            var homepage = await _context.Homepages
                .FirstOrDefaultAsync(m => m.PageId == id);
            if (homepage == null)
            {
                return NotFound();
            }

            return View(homepage);
        }

        // POST: Homepages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Homepages == null)
            {
                return Problem("Entity set 'ModelContext.Homepages'  is null.");
            }
            var homepage = await _context.Homepages.FindAsync(id);
            if (homepage != null)
            {
                _context.Homepages.Remove(homepage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomepageExists(decimal id)
        {
          return (_context.Homepages?.Any(e => e.PageId == id)).GetValueOrDefault();
        }
    }
}
