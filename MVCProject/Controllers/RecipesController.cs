﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RecipesController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Recipes
        public async Task<IActionResult> Index( )
        {

            var userid = HttpContext.Session.GetInt32("ChefId");

            var modelContext = _context.Recipes.Where(x => x.UserId == userid).Include(r => r.Cat).Include(r => r.User);
            return View(await modelContext.ToListAsync());
           
        }

        // GET: Recipes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Cat)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RecId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // GET: Recipes/Create
        public IActionResult Create()
        {
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName");
           

            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecId,UserId,CatId,Name,Price,Ingrediants,Instruction,ImageFile,Status")] Recipe recipe)
        {  
            if (ModelState.IsValid)
            {

                if (recipe.ImageFile != null)
                {
                    string wwwrootPath = _webHostEnvironment.WebRootPath;
                    string imageName = Guid.NewGuid().ToString() + "_" + recipe.ImageFile.FileName;
                    string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        recipe.ImageFile.CopyToAsync(fileStream);
                    }
                    recipe.Image = imageName;
                }
                
                recipe.UserId= HttpContext.Session.GetInt32("ChefId");

                recipe.Dateadd = DateTime.Now;
                _context.Add(recipe);
                await _context.SaveChangesAsync();
                TempData["message"] = "You Are Successfully Added your recipe to MasterChef";
                return RedirectToAction("Index", "Recipes");
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", recipe.CatId);
            
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatName", recipe.CatId);
           
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("RecId,UserId,CatId,Name,Price,Ingrediants,Instruction,ImageFile,Status")] Recipe recipe)
        {
            if (id != recipe.RecId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    if (recipe.ImageFile != null)
                    {
                        string wwwrootPath = _webHostEnvironment.WebRootPath;
                        string imageName = Guid.NewGuid().ToString() + "_" + recipe.ImageFile.FileName;
                        string fullPath = Path.Combine(wwwrootPath + "/Images/", imageName);
                        using (var fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                            recipe.ImageFile.CopyToAsync(fileStream);
                        }
                        recipe.Image = imageName;
                    }

                    recipe.UserId = HttpContext.Session.GetInt32("ChefId");
                    recipe.Dateadd = DateTime.Now;
                    
                    _context.Update(recipe);
                    await _context.SaveChangesAsync();
                    TempData["message"] = "You Are Successfully Edit your recipe to MasterChef";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipeExists(recipe.RecId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                 return RedirectToAction("Index", "Recipes"); 

            }
            ViewData["CatId"] = new SelectList(_context.Categories, "CatId", "CatId", recipe.CatId);
           
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Recipes == null)
            {
                return NotFound();
            }

            var recipe = await _context.Recipes
                .Include(r => r.Cat)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RecId == id);
            if (recipe == null)
            {
                return NotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'ModelContext.Recipes'  is null.");
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
            }
            
            await _context.SaveChangesAsync();
            TempData["message"] = "You Are Successfully Delete your recipe to MasterChef";
            return RedirectToAction("Index", "Recipes");
        }

        private bool RecipeExists(decimal id)
        {
          return (_context.Recipes?.Any(e => e.RecId == id)).GetValueOrDefault();
        }
    }
}
