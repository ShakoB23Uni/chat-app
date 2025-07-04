using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using uni_project_chat_app.Data;
using uni_project_chat_app.Models;

namespace uni_project_chat_app.Hubs
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ChatUser> _userManager;

        public ChatHub(ApplicationDbContext context, UserManager<ChatUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task SendMessage(string receiverId, string message)
        {
            var senderId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(message.Trim()))
                return;

            var sender = await _userManager.FindByIdAsync(senderId);
            var receiver = await _userManager.FindByIdAsync(receiverId);
            
            if (sender == null || receiver == null)
                return;

            // Save message to database
            var chatMessage = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = message.Trim(),
                Timestamp = DateTime.UtcNow,
                MessageType = MessageType.Text
            };

            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Send message to receiver
            await Clients.User(receiverId).SendAsync("ReceiveMessage", 
                senderId, sender.DisplayName, message, chatMessage.Timestamp.ToString("HH:mm"));

            // Send message back to sender for confirmation
            await Clients.Caller.SendAsync("MessageSent", 
                receiverId, receiver.DisplayName, message, chatMessage.Timestamp.ToString("HH:mm"));
        }

        public async Task SendMessageToAll(string message)
        {
            var senderId = Context.UserIdentifier;
            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(message.Trim()))
                return;

            var sender = await _userManager.FindByIdAsync(senderId);
            if (sender == null)
                return;

            // Get all users except sender
            var allUsers = await _userManager.Users.Where(u => u.Id != senderId).ToListAsync();
            
            // Save message to database for each user
            foreach (var user in allUsers)
            {
                var chatMessage = new Message
                {
                    SenderId = senderId,
                    ReceiverId = user.Id,
                    Content = message.Trim(),
                    Timestamp = DateTime.UtcNow,
                    MessageType = MessageType.Text
                };
                _context.Messages.Add(chatMessage);
            }
            await _context.SaveChangesAsync();

            // Send message to all connected clients
            await Clients.All.SendAsync("ReceiveMessage", 
                senderId, sender.DisplayName, message, DateTime.UtcNow.ToString("HH:mm"));
        }

        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsOnline = true;
                    user.LastSeen = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);

                    // Notify all clients about user coming online
                    await Clients.All.SendAsync("UserOnline", userId, user.DisplayName);
                }
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    user.IsOnline = false;
                    user.LastSeen = DateTime.UtcNow;
                    await _userManager.UpdateAsync(user);

                    // Notify all clients about user going offline
                    await Clients.All.SendAsync("UserOffline", userId, user.DisplayName);
                }
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
} 