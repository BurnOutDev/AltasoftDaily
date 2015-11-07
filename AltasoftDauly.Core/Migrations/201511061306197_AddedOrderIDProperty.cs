namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedOrderIDProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "OrderID", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyPayments", "OrderID");
        }
    }
}
