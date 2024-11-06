using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Data;
using MySite.Models;

public class CatalogController : Controller
{
    private readonly MySiteContext _context;

    public CatalogController(MySiteContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(decimal? minPrice, decimal? maxPrice, bool? inStock, string searchQuery, int[] categoryIds, int[] brandIds, int[] animalTypeIds)
    {
        var categories = await _context.Category.ToListAsync();
        var brands = await _context.Brand.ToListAsync();
        var animalTypes = await _context.AnimalType.ToListAsync();

        ViewData["Categories"] = categories;
        ViewData["Brands"] = brands;
        ViewData["AnimalTypes"] = animalTypes;

        var products = await _context.Product
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.AnimalType)
            .ToListAsync();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            products = products.Where(p => p.Name.Contains(searchQuery)).ToList();
        }

        if (minPrice.HasValue || maxPrice.HasValue)
        {
            products = products.Where(p =>
                (!minPrice.HasValue || p.Price >= minPrice.Value) &&
                (!maxPrice.HasValue || p.Price <= maxPrice.Value)).ToList();
        }


        if (inStock.HasValue && inStock.Value)
        {
            products = products.Where(p => p.Stock > 0).ToList();
        }

        if (categoryIds != null && categoryIds.Length > 0)
        {
            products = products.Where(p => categoryIds.Contains(p.CategoryId)).ToList();
        }

        if (brandIds != null && brandIds.Length > 0)
        {
            products = products.Where(p => brandIds.Contains(p.BrandId)).ToList();
        }

        if (animalTypeIds != null && animalTypeIds.Length > 0)
        {
            products = products.Where(p => animalTypeIds.Contains(p.AnimalTypeId)).ToList();
        }

        return View(products);
    }
}
