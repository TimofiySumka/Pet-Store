using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MySite.Areas.Identity.Data;
using System.Threading.Tasks;

namespace MySite.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // GET: /Users
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users;
            return View(users);
        }
    }
}
