using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.DTO.Auth
{
    public class ChangePasswordRequestDTO
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "Confirm Password must match the New Password.")]
        public string ConfirmPassword { get; set; }
    }
}
