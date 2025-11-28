using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.DOMAIN.model
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Type { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public ApplicationUser? User { get; set; }
    }
}
