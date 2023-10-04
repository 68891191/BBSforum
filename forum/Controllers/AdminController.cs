using System.ComponentModel.DataAnnotations;
using Forum.Data;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;

namespace forum.Controllers
{
    public class AdminController : Controller
    {
        private readonly ForumDbContext _context;

        public AdminController(ForumDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Get the user's authentication token from the session
            string? token = HttpContext.Session.GetString("token");

            // If the user is not authenticated, redirect to the SignIn action in the Auth controller
            if (token == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // Retrieve the user associated with the token from the database
            var user = _context.User.FirstOrDefault(u => u.token == HttpContext.Session.GetString("token"));

            // If the user doesn't exist in the database, return a problem response
            if (user == null)
            {
                return Problem("User does not exist.");
            }

            // If the user's role is not 'admin', redirect to the SignIn action in the Auth controller
            if (user.role != "admin")
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // If all conditions are met, return the default view for the Admin controller
            return View();
        }
    }
}
