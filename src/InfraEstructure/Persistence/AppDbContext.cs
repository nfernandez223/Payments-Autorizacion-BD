using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfraEstructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Solicitud> Solicitud { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
