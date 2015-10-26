namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFiltersAndRemovedBranchesAndEditedPOCOClasses : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Actions", newName: "UserActions");
            DropForeignKey("dbo.Users", "Branch_BranchID", "dbo.Branches");
            DropForeignKey("dbo.Users", "User_UserID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Branch_BranchID" });
            DropIndex("dbo.Users", new[] { "User_UserID" });
            CreateTable(
                "dbo.Filters",
                c => new
                    {
                        FilterID = c.Int(nullable: false, identity: true),
                        Enabled = c.Boolean(nullable: false),
                        IsDeptFilterEnabled = c.Boolean(nullable: false),
                        IsOperatorFilterEnabled = c.Boolean(nullable: false),
                        IsCustomerFilterEnabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.FilterID);
            
            AddColumn("dbo.Users", "Filter_FilterID", c => c.Int());
            CreateIndex("dbo.Users", "Filter_FilterID");
            AddForeignKey("dbo.Users", "Filter_FilterID", "dbo.Filters", "FilterID");
            DropColumn("dbo.Users", "Branch_BranchID");
            DropColumn("dbo.Users", "User_UserID");
            DropColumn("dbo.UserActions", "DeptID");
            DropTable("dbo.Branches");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BranchID);
            
            AddColumn("dbo.UserActions", "DeptID", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "User_UserID", c => c.Int());
            AddColumn("dbo.Users", "Branch_BranchID", c => c.Int());
            DropForeignKey("dbo.Users", "Filter_FilterID", "dbo.Filters");
            DropIndex("dbo.Users", new[] { "Filter_FilterID" });
            DropColumn("dbo.Users", "Filter_FilterID");
            DropTable("dbo.Filters");
            CreateIndex("dbo.Users", "User_UserID");
            CreateIndex("dbo.Users", "Branch_BranchID");
            AddForeignKey("dbo.Users", "User_UserID", "dbo.Users", "UserID");
            AddForeignKey("dbo.Users", "Branch_BranchID", "dbo.Branches", "BranchID");
            RenameTable(name: "dbo.UserActions", newName: "Actions");
        }
    }
}
