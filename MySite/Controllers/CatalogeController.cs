using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MySite.Areas.Identity.Data;
using MySite.Data;
using MySite.Models;

public class CatalogController : Controller
{
    private readonly MySiteContext _context;
    private readonly UserManager<User> _userManager;

    public CatalogController(MySiteContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }


    [HttpPost]

    private async Task BadgeCount()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["WishlistCount"] = await _context.Wishlist.CountAsync(w => w.UserId == user.Id);
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == user.Id);
            ViewData["CartCount"] = cart?.CartItems.Count ?? 0;
        }
        else
        {
            ViewData["WishlistCount"] = 0;
            ViewData["CartCount"] = 0;
        }
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        await BadgeCount();
        await next(); // продолжить выполнение действия после вызова BadgeCount
    }

    public async Task<IActionResult> AddToWishlist(int productId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return Unauthorized();
        }

        var existingWishlistItem = await _context.Wishlist
            .FirstOrDefaultAsync(w => w.UserId == user.Id && w.ProductId == productId);

        if (existingWishlistItem == null)
        {
            var wishlistItem = new Wishlist
            {
                UserId = user.Id,
                ProductId = productId
            };
            _context.Wishlist.Add(wishlistItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
        else
        {
            _context.Wishlist.Remove(existingWishlistItem);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }



    public async Task<IActionResult> Index(decimal? minPrice, decimal? maxPrice, bool? inStock, string searchQuery, int[] categoryIds, int[] brandIds, int[] animalTypeIds)
    {
        var categories = await _context.Category.ToListAsync();
        var brands = await _context.Brand.ToListAsync();
        var animalTypes = await _context.AnimalType.ToListAsync();

        ViewData["Categories"] = categories;
        ViewData["Brands"] = brands;
        ViewData["AnimalTypes"] = animalTypes;
        ViewData["MinPrice"] = minPrice;
        ViewData["MaxPrice"] = maxPrice;
        ViewData["InStock"] = inStock;
        ViewData["SelectedCategoryIds"] = categoryIds;
        ViewData["SelectedBrandIds"] = brandIds;
        ViewData["SelectedAnimalTypeIds"] = animalTypeIds;
        ViewData["SearchQuery"] = searchQuery;

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
