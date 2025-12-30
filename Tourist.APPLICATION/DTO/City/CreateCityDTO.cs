using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.DTO.City
{
    public class CreateCityDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public int CountryId { get; set; }
    }
}
