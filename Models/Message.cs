using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace uni_project_chat_app.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = string.Empty;

        [Required]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required]
        public string SenderId { get; set; } = string.Empty;

        [Required]
        public string ReceiverId { get; set; } = string.Empty;

        [Display(Name = "Is Read")]
        public bool IsRead { get; set; } = false;

        [Display(Name = "Message Type")]
        public MessageType MessageType { get; set; } = MessageType.Text;

        // Navigation properties
        [ForeignKey("SenderId")]
        public virtual ChatUser Sender { get; set; } = null!;

        [ForeignKey("ReceiverId")]
        public virtual ChatUser Receiver { get; set; } = null!;
    }

    public enum MessageType
    {
        Text,
        System,
        Join,
        Leave
    }
} 