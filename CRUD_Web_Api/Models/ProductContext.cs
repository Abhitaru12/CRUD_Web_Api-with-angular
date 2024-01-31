using Microsoft.EntityFrameworkCore;

namespace CRUD_Web_Api.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
