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
        public DbSet<TranslatedBanlistType> TranslatedBanlistType { get; set; }
        public DbSet<TranslatedMonsterCardType> TranslatedMonsterCardType { get; set; }
        public DbSet<TranslatedMonsterType> TranslatedMonsterType { get; set; }
        public DbSet<TranslatedRarityType> TranslatedRarityType { get; set; }
        public DbSet<TranslatedSpecialMonsterType> TranslatedSpecialMonsterType { get; set; }
        public DbSet<TranslatedSpellCardType> TranslatedSpellCardType { get; set; }
        public DbSet<TranslatedTrapCardType> TranslatedTrapCardType { get; set; }
    }
}
