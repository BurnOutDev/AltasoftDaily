namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDepartmentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DeptId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DeptId");
        }
    }
}
