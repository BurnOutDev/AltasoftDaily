namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLogging : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserActions", "User_UserID", "dbo.Users");
            DropIndex("dbo.UserActions", new[] { "User_UserID" });
            CreateTable(
                "dbo.CommentLogs",
                c => new
                    {
                        CommentLogID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        CommentValue = c.String(),
                        LocalPayment_DailyPaymentID = c.Int(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.CommentLogID)
                .ForeignKey("dbo.DailyPayments", t => t.LocalPayment_DailyPaymentID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.LocalPayment_DailyPaymentID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        ExceptionLogID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Message = c.String(),
                        Source = c.String(),
                        StackTrace = c.String(),
                        InnerException_ExceptionLogID = c.Int(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ExceptionLogID)
                .ForeignKey("dbo.ExceptionLogs", t => t.InnerException_ExceptionLogID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.InnerException_ExceptionLogID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.OrderLogs",
                c => new
                    {
                        OrderLogID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        OrderID = c.Long(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LocalPayment_DailyPaymentID = c.Int(),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderLogID)
                .ForeignKey("dbo.DailyPayments", t => t.LocalPayment_DailyPaymentID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.LocalPayment_DailyPaymentID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.SignLogs",
                c => new
                    {
                        OrderLogID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        InternalIP = c.String(),
                        ExternalIP = c.String(),
                        InternalUsername = c.String(),
                        ExternalUsername = c.String(),
                        SignType = c.Int(nullable: false),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.OrderLogID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
            DropTable("dbo.UserActions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserActions",
                c => new
                    {
                        ActionID = c.Int(nullable: false, identity: true),
                        RequestID = c.String(),
                        OrderID = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DocNum = c.String(),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ActionID);
            
            DropForeignKey("dbo.SignLogs", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.OrderLogs", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.OrderLogs", "LocalPayment_DailyPaymentID", "dbo.DailyPayments");
            DropForeignKey("dbo.ExceptionLogs", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.ExceptionLogs", "InnerException_ExceptionLogID", "dbo.ExceptionLogs");
            DropForeignKey("dbo.CommentLogs", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.CommentLogs", "LocalPayment_DailyPaymentID", "dbo.DailyPayments");
            DropIndex("dbo.SignLogs", new[] { "User_UserID" });
            DropIndex("dbo.OrderLogs", new[] { "User_UserID" });
            DropIndex("dbo.OrderLogs", new[] { "LocalPayment_DailyPaymentID" });
            DropIndex("dbo.ExceptionLogs", new[] { "User_UserID" });
            DropIndex("dbo.ExceptionLogs", new[] { "InnerException_ExceptionLogID" });
            DropIndex("dbo.CommentLogs", new[] { "User_UserID" });
            DropIndex("dbo.CommentLogs", new[] { "LocalPayment_DailyPaymentID" });
            DropTable("dbo.SignLogs");
            DropTable("dbo.OrderLogs");
            DropTable("dbo.ExceptionLogs");
            DropTable("dbo.CommentLogs");
            CreateIndex("dbo.UserActions", "User_UserID");
            AddForeignKey("dbo.UserActions", "User_UserID", "dbo.Users", "UserID");
        }
    }
}
