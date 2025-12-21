using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.DTO.Notification
{
    public class CreateNotificationDTOs
    {
        public string? Message { get; set; }
        public bool IsRead { get; set; }
        public string? Type { get; set; }

        public string UserId { get; set; }
    }
}
