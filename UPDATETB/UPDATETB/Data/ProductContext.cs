using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UPDATETB.Models;

namespace UPDATETB.Data
{
    public class ProductContext : IdentityDbContext
    {
        public ProductContext(DbContextOptions<ProductContext>
        options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
