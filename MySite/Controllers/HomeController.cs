using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Models;
using MySite.Models.ViewModels;
using MySite.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using MySite.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MySiteContext _context;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, MySiteContext context, UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

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
            await next(); 
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.AnimalType)
                .ToListAsync();

            return View(products);
        }


        public IActionResult About()
        {

            return View();
        }

        public async Task<IActionResult> Wishlist()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Redirect("https://localhost:7017/Identity/Account/Login");
            }

            var wishlistItems = await _context.Wishlist
                .Include(w => w.Product)
                .Where(w => w.UserId == user.Id)
                .ToListAsync();

            return View(wishlistItems);
        }

        public async Task<IActionResult> Cart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                ViewData["CartExists"] = false;
                return View();
            }

            ViewData["CartExists"] = true;
            ViewData["IsCartEmpty"] = !cart.CartItems.Any();


            return View(cart);
        }

        public async Task<IActionResult> History()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Order
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product) 
                .ToListAsync();

            var model = new OrderHistoryViewModel
            {
                Orders = orders
            };

            return View("History", model);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
