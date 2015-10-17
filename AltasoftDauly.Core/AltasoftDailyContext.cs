using AltasoftDaily.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Core
{
    public class AltasoftDailyContext : DbContext
    {
        public AltasoftDailyContext() : base("name=AltasoftDailyDatabaseConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AltasoftDailyContext>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
    }

    
}
