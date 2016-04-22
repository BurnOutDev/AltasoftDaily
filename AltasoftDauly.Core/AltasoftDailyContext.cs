using AltasoftDaily.Domain.POCO;
using AltasoftDaily.Domain.POCO.Logging;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace AltasoftDaily.Core
{
    public class AltasoftDailyContext : DbContext
    {
        public AltasoftDailyContext() : base("name=AltasoftDailyDatabaseConnectionString")
        {
            Database.SetInitializer(new CustomDatabaseInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DbSet<DailyPayment> DailyPayments { get; set; }
        public DbSet<LoanAccountNumber> AccountNumbers { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }
        public DbSet<CommentLog> CommentLogs { get; set; }
        public DbSet<OrderLog> OrderLogs { get; set; }
        public DbSet<SignLog> SignLogs { get; set; }
        public DbSet<EnforcementLoan> EnforcementLoans { get; set; }
        public DbSet<AgreementAndSummaryJudgementTerms> AgreementAndSummaryJudgementTerms { get; set; }
    }

    public class CustomDatabaseInitializer : CreateDatabaseIfNotExists<AltasoftDailyContext>
    {
        protected override void Seed(AltasoftDailyContext context)
        {
        }
    }
}
