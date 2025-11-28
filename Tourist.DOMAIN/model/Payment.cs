using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tourist.DOMAIN.model
{  
    
    public enum PaymentStatus
        {
            Pending = 1,
            Paid = 2,
            Failed = 3,
            Refunded = 4
        }
        public enum PaymentMethod
        {
            Cash = 1,
            CreditCard = 2,
            Wallet = 3,
            BankTransfer = 4
        }
    public class Payment
    {

        public int PaymentId { get; set; }
        public double Amount { get; set; }
        public PaymentMethod? Method { get; set; }
        public DateTime Date { get; set; }
        public PaymentStatus? Status { get; set; }
        public string? Currency { get; set; }

        [ForeignKey("Trip")]
        public int TripId { get; set; }
        public Trip? Trip { get; set; }
    }
}
