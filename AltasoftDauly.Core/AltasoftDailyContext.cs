using AltasoftDaily.Domain.POCO;
using System.Data.Entity;

namespace AltasoftDaily.Core
{
    public class AltasoftDailyContext : DbContext
    {
        public AltasoftDailyContext() : base("name=AltasoftDailyDatabaseConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AltasoftDailyContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAction> UserActions { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<DailyPayment> DailyPayments { get; set; }
    }
}
