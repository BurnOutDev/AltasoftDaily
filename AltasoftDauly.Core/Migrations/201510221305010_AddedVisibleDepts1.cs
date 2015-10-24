namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVisibleDepts1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PrivateNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PrivateNumber");
        }
    }
}
