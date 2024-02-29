using GameOfChangeCqrs.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameOfChanceCqrs.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Player> Players { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
