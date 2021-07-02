using Microsoft.EntityFrameworkCore;
using tiko_net_assignment.Models;

namespace tiko_net_assignment.DataContext
{
    public class AppContext : DbContext
    {
        public AppContext() { }
        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<House> Houses { get; set; }
    }
}
