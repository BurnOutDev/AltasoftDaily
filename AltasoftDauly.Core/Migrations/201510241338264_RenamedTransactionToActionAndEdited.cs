namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedTransactionToActionAndEdited : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "UserID", "dbo.Users");
            DropIndex("dbo.Transactions", new[] { "UserID" });
            CreateTable(
                "dbo.Actions",
                c => new
                    {
                        ActionID = c.Int(nullable: false, identity: true),
                        RequestID = c.String(),
                        OrderID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DocNum = c.String(),
                        DeptID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ActionID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
            DropTable("dbo.Transactions");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.TransactionID);
            
            DropForeignKey("dbo.Actions", "User_UserID", "dbo.Users");
            DropIndex("dbo.Actions", new[] { "User_UserID" });
            DropTable("dbo.Actions");
            CreateIndex("dbo.Transactions", "UserID");
            AddForeignKey("dbo.Transactions", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}
