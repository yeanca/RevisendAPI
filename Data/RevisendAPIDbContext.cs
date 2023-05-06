using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RevisendAPI.Data.Entities;

namespace RevisendAPI.Data
{
    public class RevisendAPIDbContext:IdentityDbContext<User>
    {
        public RevisendAPIDbContext(DbContextOptions option):base(option)
        {
            
        }
        
    }
}
