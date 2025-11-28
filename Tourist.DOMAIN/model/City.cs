using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.DOMAIN.model
{
    public class City
    {
        public int CityId { get; set; }
        public string? Name { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public IEnumerable<Hotel>? Hotels { get; set; }
        public IEnumerable<Place>? Places { get; set; }
        public IEnumerable<Trip>? Trips { get; set; }

    }
}
