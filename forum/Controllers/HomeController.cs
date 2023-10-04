using System.Diagnostics;
using Forum.Data;
using Microsoft.AspNetCore.Mvc;
using forum.Models;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using SQLitePCL;

namespace forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ForumDbContext _context;

        public HomeController(ILogger<HomeController> logger, ForumDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Handles the main page display
        public IActionResult Index()
        {
            string? token = HttpContext.Session.GetString("token");

            // If the user is not authenticated, show the default view
            if (token == null)
            {
                return View();
            }

            // If the user is authenticated, display the "IndexLoggedIn" view
            var userSession = _context.User.FirstOrDefault(u => u.token == token);
            if (userSession == null)
            {
                return Problem("User does not exist.");
            }

            // Retrieve and display posts along with user and tag information
            List<Post> posts = new List<Post>();
            posts = _context.Post.OrderByDescending(p => p.createdAt).ToList();
            ViewBag.posts = posts;

            var userIdToName = new Dictionary<int, string>();
            foreach (var post in posts)
            {
                var user = _context.User.Find(post.userId);
                if (user != null)
                    userIdToName[post.userId] = user.name + " " + user.lastName;
            }
            ViewData["userIdToName"] = userIdToName;

            var tagIdToName = new Dictionary<int, string>();
            foreach (var post in posts)
            {
                var tag = _context.Tag.Find(post.tagId);
                if (tag != null)
                    tagIdToName[post.tagId] = tag.name;
            }
            ViewData["tagIdToName"] = tagIdToName;

            ViewBag.userEmail = userSession.email;
            ViewBag.role = userSession.role;

            return View("IndexLoggedIn");
        }

        // Handles search functionality
        public IActionResult Search(string query)
        {
            // Check if the user is authenticated
            var user = _context.User.FirstOrDefault(u => u.token == HttpContext.Session.GetString("token"));
            if (user == null)
            {
                return RedirectToAction("SignIn", "Auth");
            }

            // Search for posts that match the query or have a matching tag
            var tag = _context.Tag.FirstOrDefault(t => t.name == query);
            int tagId = tag != null ? tag.id : -1;

            var posts = _context.Post.Where(p => p.title.Contains(query) || p.tagId == tagId).ToList();
            ViewBag.posts = posts;
            ViewBag.postsCount = posts.Count;
            ViewBag.query = query;

            var userIdToName = new Dictionary<int, string>();
            foreach (var post in posts)
            {
                var userPost = _context.User.Find(post.userId);
                if (userPost != null)
                    userIdToName[post.userId] = userPost.name + " " + userPost.lastName;
            }
            ViewData["userIdToName"] = userIdToName;

            var tagIdToName = new Dictionary<int, string>();
            foreach (var post in posts)
            {
                var tagPost = _context.Tag.Find(post.tagId);
                if (tagPost != null)
                    tagIdToName[post.tagId] = tagPost.name;
            }
            ViewData["tagIdToName"] = tagIdToName;

            return View();
        }

        // Handles error page display
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
