using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.DTO.Auth
{
    public class LoginDTOs
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string? ClientUri { get; set; }

    }
}
