using System;
using System.Collections.Generic;

namespace RevisendAPI.Data
{
    public partial class UserAddress
    {
        public int AddressId { get; set; }
        public int UserId { get; set; }
        public string Country { get; set; } = null!;
        public string Address { get; set; } = null!;
    }
}
