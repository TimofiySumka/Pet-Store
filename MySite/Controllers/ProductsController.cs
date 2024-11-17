using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySite.Data;
using MySite.Models;
using Microsoft.Extensions.Logging;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace MySite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly MySiteContext _context;



        public ProductsController(MySiteContext context, ILogger<ProductsController> logger)
        {
            _context = context;


        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var mySiteContext = _context.Product.Include(p => p.AnimalType).Include(p => p.Brand).Include(p => p.Category);
            return View(await mySiteContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.AnimalType)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }



        // GET: Products/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,FullDescription,Price,DiscountPrecent,Stock,CreatedDate,ImageUrl,CategoryId,BrandId,AnimalTypeId,AgeCategory,ProductSize,ProductWeight")] Product product, IFormFile imageFile)
        {

            if (!ModelState.IsValid)
            {

                PopulateDropdowns(product.CategoryId, product.BrandId, product.AnimalTypeId);
                return View(product);
            }


            if (imageFile != null && imageFile.Length > 0)
            {
                try
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", imageFile.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }
                    product.ImageUrl = "/uploads/" + imageFile.FileName;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("ImageUrl", "Ошибка при загрузке файла.");
                    PopulateDropdowns(product.CategoryId, product.BrandId, product.AnimalTypeId);
                    return View(product);
                }
            }

            try
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                PopulateDropdowns(product.CategoryId, product.BrandId, product.AnimalTypeId);
                return View(product);
            }


        }

        private void PopulateDropdowns(int? selectedCategoryId = null, int? selectedBrandId = null, int? selectedAnimalTypeId = null)
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", selectedCategoryId);
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", selectedBrandId);
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Name", selectedAnimalTypeId);
        }

        // GET: Products/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Name", product.AnimalTypeId);
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,FullDescription,Price,DiscountPrecent,Stock,CreatedDate,ImageUrl,CategoryId,BrandId,AnimalTypeId,AgeCategory,ProductSize,ProductWeight")] Product product, IFormFile imageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", imageFile.FileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        product.ImageUrl = "/uploads/" + imageFile.FileName;
                    }

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Name", product.AnimalTypeId);
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", product.BrandId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.AnimalType)
                .Include(p => p.Brand)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}