using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace uni_project_chat_app.Models
{
    public class ChatUser : IdentityUser
    {
        [Required]
        [Display(Name = "Display Name")]
        [StringLength(50, MinimumLength = 2)]
        public string DisplayName { get; set; } = string.Empty;

        [Display(Name = "Is Online")]
        public bool IsOnline { get; set; } = false;

        [Display(Name = "Last Seen")]
        public DateTime LastSeen { get; set; } = DateTime.UtcNow;

        [Display(Name = "Join Date")]
        public DateTime JoinDate { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<Message> SentMessages { get; set; } = new HashSet<Message>();
        public virtual ICollection<Message> ReceivedMessages { get; set; } = new HashSet<Message>();
    }
} 