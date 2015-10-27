namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedCalculationDateFromStringToDate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DailyPayments", "CalculationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DailyPayments", "CalculationDate", c => c.String());
        }
    }
}
