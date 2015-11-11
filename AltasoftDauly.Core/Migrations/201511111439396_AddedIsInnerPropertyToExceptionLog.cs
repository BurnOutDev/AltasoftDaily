namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsInnerPropertyToExceptionLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExceptionLogs", "IsInner", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExceptionLogs", "IsInner");
        }
    }
}
