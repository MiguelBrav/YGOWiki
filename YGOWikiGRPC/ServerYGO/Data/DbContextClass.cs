using Microsoft.EntityFrameworkCore;
using ServerYGO.Data.Entities;

namespace ServerYGO.Data
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<TranslatedCardTypes> TranslatedCardTypes { get; set; }
        public DbSet<TranslatedAttribute> TranslatedAttribute { get; set; }
    }
}
