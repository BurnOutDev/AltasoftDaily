namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLatePrincipal : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "LatePrincipalInGel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyPayments", "LatePrincipalInGel");
        }
    }
}
