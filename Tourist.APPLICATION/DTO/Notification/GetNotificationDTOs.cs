using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.DTO.Notification
{
    public class GetNotificationDTOs
    {
        public int NotificationId { get; set; }
        public string? Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Type { get; set; }

        public string UserId { get; set; }
    }
}
