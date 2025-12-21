using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model.Shared;

namespace Tourist.DOMAIN.model
{
    public class Country: BaseEntity
    {
        public int CountryId { get; set; }
        public string? Name { get; set; }
        public IEnumerable<City>? Cities { get; set; }
    }
}
