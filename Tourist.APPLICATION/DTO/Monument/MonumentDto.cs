using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.APPLICATION.DTO.Monument
{
    public class MonumentDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Location { get; set; }
        public int YearBuilt { get; set; }
    }
}
