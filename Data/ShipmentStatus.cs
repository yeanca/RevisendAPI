using System;
using System.Collections.Generic;

namespace RevisendAPI.Data
{
    public partial class ShipmentStatus
    {
        public int StatusId { get; set; }
        public string Status { get; set; } = null!;

        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
