using System;
using System.Collections.Generic;

namespace RevisendAPI.Data
{
    public partial class UserShipmentDTO
    {
        public int ShipmentNo { get; set; }
        public int ByUser { get; set; }
        public string? SourceCountry { get; set; }
        public string? SourceStore { get; set; }
        public DateTime CreatedAt { get; set; }
        public float? Weight { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? DateDelivered { get; set; }
        public DateTime? DateEta { get; set; }
        public string Status { get; set; }
    }
}
