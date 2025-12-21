using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tourist.DOMAIN.model.Shared;

namespace Tourist.DOMAIN.model
{
    public class TripActivity: BaseEntity
    {
        [Key]
        public int ActivityId { get; set; }
        public string? Notes { get; set; }
        public DateTime VisitDate { get; set; }

        [ForeignKey("Trip")]
        public int TripId { get; set; }

        [ForeignKey("Place")]
        public int PlaceId { get; set; }
        public Trip? Trip { get; set; }
        public Place? Place { get; set; }
    }
}
