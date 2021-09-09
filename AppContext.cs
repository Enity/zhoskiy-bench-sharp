using Microsoft.EntityFrameworkCore;
using ZhoskiyBenchSharp.Models;

namespace ZhoskiyBenchSharp
{
    public class AppContext : DbContext
    {
        public DbSet<Bear> Bears { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }
    }
}