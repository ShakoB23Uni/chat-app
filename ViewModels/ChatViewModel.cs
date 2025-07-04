using uni_project_chat_app.Models;

namespace uni_project_chat_app.ViewModels
{
    public class ChatViewModel
    {
        public ChatUser CurrentUser { get; set; } = null!;
        public IList<ChatUser> Users { get; set; } = new List<ChatUser>();
        public IList<Message> RecentMessages { get; set; } = new List<Message>();
    }
} 