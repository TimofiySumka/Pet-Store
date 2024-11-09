using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Models;
using MySite.Data;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using MySite.Areas.Identity.Data;

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

        public IActionResult Cart()
        {
            return View();
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
