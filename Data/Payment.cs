using System;
using System.Collections.Generic;

namespace RevisendAPI.Data
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int ByUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? ShipmentId { get; set; }
        public decimal? Amount { get; set; }
        public int? Status { get; set; }
    }
}
