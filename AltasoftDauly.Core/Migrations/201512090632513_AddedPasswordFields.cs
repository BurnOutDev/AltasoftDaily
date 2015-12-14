namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPasswordFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastPasswordChange", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "ForceUserToChangePassword", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ForceUserToChangePassword");
            DropColumn("dbo.Users", "LastPasswordChange");
        }
    }
}
