using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using uni_project_chat_app.Data;
using uni_project_chat_app.Models;
using uni_project_chat_app.ViewModels;

namespace uni_project_chat_app.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Client-side authentication will handle access control
            return View();
        }

        [HttpGet]
        public IActionResult GetOnlineUsers()
        {
            // Return empty list for now - client-side will handle user management
            return Json(new List<object>());
        }

        [HttpGet]
        public IActionResult GetMessages(string userId, int skip = 0, int take = 20)
        {
            // Return empty list for now - client-side will handle message storage
            return Json(new List<object>());
        }

        [HttpPost]
        public IActionResult MarkAsRead(string userId)
        {
            // Client-side will handle message read status
            return Ok();
        }
    }
} 