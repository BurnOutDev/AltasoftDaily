namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_IsSelected_column : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "IsSelected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyPayments", "IsSelected");
        }
    }
}
