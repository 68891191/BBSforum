using System.ComponentModel.DataAnnotations;
using Forum.Data;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace forum.Controllers
{
    public class AuthController : Controller
    {
        private readonly ForumDbContext _context;

        public AuthController(ForumDbContext context)
        {
            _context = context;
        }

        // Displays an error message
        public IActionResult Error(string message)
        {
            ViewBag.message = message;
            return View();
        }

        // Retrieves a user asynchronously from the session token
        private async Task<User?> GetUserAsync()
        {
            string? token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return null;
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.token == token);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        // Displays the sign-in view
        public IActionResult SignIn()
        {
            return View();
        }

        // Logs the user out by clearing the session
        public async Task<IActionResult> Logout()
        {
            var user = await GetUserAsync();
            if (user != null)
            {
                user.token = null;
                _context.User.Update(user);
                _context.SaveChanges();
            }
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        // Handles the sign-in form submission
        [HttpPost]
        public IActionResult SignIn(IFormCollection form)
        {
            // Extracts user input (email and password) from the form
            var email = form["email"].ToString();
            var password = form["password"].ToString();
            var user = _context.User.FirstOrDefault(u => u.email == email);

            if (user == null)
            {
                return View("Error", "User does not exist.");
            }

            // Verifies the user's password using BCrypt
            var authResult = BCrypt.Net.BCrypt.Verify(password, user.passwordHash);

            if (!authResult)
            {
                return View("Error", "Invalid password.");
            }

            // Generates a unique token, sets it in the session, and updates the user's token in the database
            string token = Guid.NewGuid().ToString();
            HttpContext.Session.SetString("token", token);
            user.token = token;
            _context.User.Update(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        // Displays the sign-up view
        public IActionResult SignUp()
        {
            return View();
        }

        // Handles the sign-up form submission
        [HttpPost]
        public IActionResult SignUp(IFormCollection form)
        {
            // Extracts user input (email, password, etc.) from the form
            var email = form["email"].ToString();
            var password = form["password"].ToString();

            // Validates email format and password length
            if (!new EmailAddressAttribute().IsValid(email))
            {
                return View("Error", "Invalid email.");
            }
            if (password.Length < 6)
            {
                return View("Error", "Password must be at least 6 characters long.");
            }
            else if (password != form["confirmPassword"].ToString())
            {
                return View("Error", "Passwords do not match.");
            }

            // Hashes the password and checks if the user already exists
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = _context.User.FirstOrDefault(u => u.email == email);

            if (user != null)
            {
                return View("Error", "User already exists.");
            }

            // Creates a new user and adds them to the database
            var newUser = new User
            {
                email = email,
                passwordHash = passwordHash,
                token = null,
                role = "user",
                name = form["firstName"].ToString(),
                lastName = form["lastName"].ToString()
            };

            _context.Add(newUser);
            _context.SaveChanges();

            return RedirectToAction("AccountCreated");
        }

        // Displays the "AccountCreated" view
        public IActionResult AccountCreated()
        {
            return View();
        }

        // Displays the "ChangePassword" view
        public IActionResult ChangePassword()
        {
            var token = HttpContext.Session.GetString("token");

            if (token == null)
            {
                return RedirectToAction("SignIn");
            }

            return View();
        }

        // Handles the change password form submission
        [HttpPost]
        public IActionResult ChangePassword(IFormCollection form)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return RedirectToAction("SignIn");
            }

            var user = _context.User.FirstOrDefault(u => u.token == token);
            var oldPassword = form["oldPassword"].ToString();
            var newPassword = form["newPassword"].ToString();
            var confirmPassword = form["confirmPassword"].ToString();

            if (user == null)
            {
                return View("Error", "User does not exist.");
            }

            // Verifies the old password and validates the new password
            var authResult = BCrypt.Net.BCrypt.Verify(oldPassword, user.passwordHash);

            if (!authResult)
            {
                return View("Error", "Invalid password.");
            }

            if (newPassword.Length < 6)
            {
                return View("Error", "Password must be at least 6 characters long.");
            }
            else if (newPassword != confirmPassword)
            {
                return View("Error", "Passwords do not match.");
            }
            else if (newPassword == oldPassword)
            {
                return View("Error", "New password must be different from the old password.");
            }

            // Hashes the new password, clears the session, and updates the user's password in the database
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.passwordHash = passwordHash;
            user.token = null;
            _context.User.Update(user);
            _context.SaveChanges();
            HttpContext.Session.Clear();

            return RedirectToAction("PasswordChanged");
        }

        // Displays the "PasswordChanged" view
        public IActionResult PasswordChanged()
        {
            return View();
        }
    }
}
