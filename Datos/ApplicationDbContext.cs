using Microsoft.EntityFrameworkCore;
using TestApis.Models;

namespace TestApis.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Producto> productos { get; set; }
        public DbSet<User> users { get; set; } 
    }
}
