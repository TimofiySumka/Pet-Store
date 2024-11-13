using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySite.Data;
using MySite.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MySite.Controllers
{
    public class CartController : Controller
    {
        private readonly MySiteContext _context;

        public CartController(MySiteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                ViewData["Message"] = "Кошик відсутній";
                return View(null);
            }

            if (!cart.CartItems.Any())
            {
                ViewData["Message"] = "Ваш кошик порожній";
                return View(null);
            }

            return View(cart);
        }

    }
}
