using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.DOMAIN.model
{
    public class ApplicationUser: IdentityUser
    {
        public string? GoogleId { get; set; }
        public string? ProfilePicture { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Trip>? Trips { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
        public IEnumerable<Notification>? Notifications { get; set; }
        public bool IsActive { get; set; }
    }
}
