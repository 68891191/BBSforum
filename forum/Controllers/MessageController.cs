using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum.Data;
using Forum.Models;

namespace forum.Controllers
{
    public class MessageController : Controller
    {
        private readonly ForumDbContext _context;

        public MessageController(ForumDbContext context)
        {
            _context = context;
        }

        // GET: Message
        public async Task<IActionResult> Index()
        {
            // Check if the user is authenticated
            if (HttpContext.Session.GetInt32("token") == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // Retrieve the user based on their session token
            var user = await _context.User.FirstOrDefaultAsync(u => u.token == HttpContext.Session.GetString("token"));

            // If the user does not exist, return an error message
            if (user == null)
            {
                return Problem("User does not exist.");
            }

            // Retrieve and display sent and received messages for the user
            var sentMessages = _context.Message.Where(m => m.senderEmail == user.email).ToList().OrderByDescending(m => m.createdAt);
            var receivedMessages = _context.Message.Where(m => m.receiverEmail == user.email).ToList().OrderByDescending(m => m.createdAt);
            ViewBag.sentMessages = sentMessages;
            ViewBag.receivedMessages = receivedMessages;

            // Return the view if the Message entity set is not null
            return _context.Message != null ? View() : Problem("Entity set 'ForumDbContext.Message' is null.");
        }

        // GET: Message/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Message/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,senderEmail,receiverEmail,content")] Message message)
        {
            // Check if the user is authenticated
            string? token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // Retrieve the user based on their session token
            var user = _context.User.FirstOrDefault(u => u.token == token);

            // If the user does not exist, return an error
            if (user == null)
            {
                return NotFound();
            }

            // Set the sender's email to the user's email
            message.senderEmail = user.email;

            // Check if sender and receiver email are provided, then add the message
            if (!string.IsNullOrEmpty(message.senderEmail) && !string.IsNullOrEmpty(message.receiverEmail))
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        // Check if a message with a specific id exists
        private bool MessageExists(int id)
        {
            return (_context.Message?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
