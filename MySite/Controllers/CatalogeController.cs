using Microsoft.AspNetCore.Mvc;
using MySite.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MySite.Controllers
{
    public class CatalogController : Controller
    {
        private readonly MySiteContext _context;

        public CatalogController(MySiteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Product.ToListAsync();
            return View(products);
        }
    }
}
