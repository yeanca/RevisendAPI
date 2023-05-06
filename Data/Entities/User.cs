using Microsoft.AspNetCore.Identity;
using System;

namespace RevisendAPI.Data.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public long AppId { get; set; }
    }
}
