using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.DTO.TourDto
{
    public class TourDto
    {
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int DurationDays { get; set; }

        //public List<int>? MonumentIds { get; set; }
    }
}
