namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.BranchID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LastName = c.String(),
                        IsLockedOut = c.Boolean(nullable: false),
                        CanSubmit = c.Boolean(nullable: false),
                        CanViewDaily = c.Boolean(nullable: false),
                        Branch_BranchID = c.Int(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Branches", t => t.Branch_BranchID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.Branch_BranchID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        RequestID = c.String(),
                        UserID = c.Int(nullable: false),
                        DeptID = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        TransactionStatus = c.Byte(nullable: false),
                        TransactionCode = c.String(),
                        OpCode = c.String(),
                        Purpose = c.String(),
                        CustomerAccountIBAN = c.String(),
                        CashOrderType = c.Byte(nullable: false),
                        DocNum = c.String(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "Branch_BranchID", "dbo.Branches");
            DropIndex("dbo.Transactions", new[] { "UserID" });
            DropIndex("dbo.Users", new[] { "User_UserID" });
            DropIndex("dbo.Users", new[] { "Branch_BranchID" });
            DropTable("dbo.Transactions");
            DropTable("dbo.Users");
            DropTable("dbo.Branches");
        }
    }
}
