using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model.Shared;

namespace Tourist.DOMAIN.model
{
    public class Place: BaseEntity
    {
        public int PlaceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime OpeningHours { get; set; }
        public double Price { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public City? City { get; set; }
        public Category? Category { get; set; }
        public IEnumerable<TripActivity>? Activities { get; set; }

    }
}
