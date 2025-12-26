using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model.Shared;

namespace Tourist.DOMAIN.model
{
    public class Tour:BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public double Price { get; set; }
        public int DurationDays { get; set; }
        //public ICollection<TourMonument> TourMonuments { get; set; } = new List<TourMonument>();
    }
}
