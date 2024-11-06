using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MySite.Data;
using MySite.Models;

namespace MySite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MySiteContext _context;

        public ProductsController(MySiteContext context)
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
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,DiscountPrecent,Stock,ImageUrl,CategoryId,BrandId,AnimalTypeId,AgeCategory,ProductSize,ProductWeight")] Product product)
        {
            // Перевірка моделі на валідність
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        TempData["ErrorMessage"] = $"Помилка у полі '{state.Key}': {state.Value.Errors.First().ErrorMessage}";
                        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
                        ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", product.BrandId);
                        ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Name", product.AnimalTypeId);
                        return View(product);
                    }
                }
            }

            try
            {
                // Перевірка наявності вибраних категорій, бренду та типу тварини
                if (_context.Category.Find(product.CategoryId) == null)
                {
                    TempData["ErrorMessage"] = "Вибрана категорія не існує.";
                    return View(product);
                }

                if (_context.Brand.Find(product.BrandId) == null)
                {
                    TempData["ErrorMessage"] = "Вибраний бренд не існує.";
                    return View(product);
                }

                if (_context.AnimalType.Find(product.AnimalTypeId) == null)
                {
                    TempData["ErrorMessage"] = "Вибраний тип тварини не існує.";
                    return View(product);
                }

                // Додавання продукту
                _context.Add(product);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Запис успішно створено!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Сталася помилка при створенні запису: {ex.Message}";
                ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", product.CategoryId);
                ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name", product.BrandId);
                ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Name", product.AnimalTypeId);
                return View(product);
            }
        }
        // GET: Products/Edit/5
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,DiscountPrecent,Stock,ImageUrl,CategoryId,BrandId,AnimalTypeId,AgeCategory,ProductSize,ProductWeight")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {


                try
                {
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
            ViewData["AnimalTypeId"] = new SelectList(_context.AnimalType, "Id", "Name");
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
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
