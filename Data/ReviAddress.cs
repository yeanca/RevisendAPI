using System;
using System.Collections.Generic;

namespace RevisendAPI.Data
{
    public partial class ReviAddress
    {
        public int AddressId { get; set; }
        public string Country { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool Status { get; set; }
    }
}
