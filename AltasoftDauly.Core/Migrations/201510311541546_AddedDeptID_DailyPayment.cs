namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDeptID_DailyPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "DeptID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyPayments", "DeptID");
        }
    }
}
