namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedConnectionStringPropertyToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ConnectionString", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ConnectionString");
        }
    }
}
