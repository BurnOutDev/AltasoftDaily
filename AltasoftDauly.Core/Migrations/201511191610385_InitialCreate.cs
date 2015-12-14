namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                "dbo.DailyPayments",
                c => new
                    {
                        DailyPaymentID = c.Int(nullable: false, identity: true),
                        ClientNo = c.Int(nullable: false),
                        LoanID = c.Int(nullable: false),
                        ClientName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PersonalID = c.String(),
                        BusinessAddress = c.String(),
                        Phone = c.String(),
                        NextScheduledPaymentInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentDebtInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalDebtInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CalculationDate = c.DateTime(nullable: false),
                        AgreementNumber = c.String(),
                        LoanCCY = c.String(),
                        StartDate = c.String(),
                        EndDate = c.String(),
                        DateOfTheNotificationLetter = c.String(),
                        ProblemManageDate = c.String(),
                        ProblemManager = c.String(),
                        DateOfEnforcement = c.String(),
                        CourtAndEnforcementFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientAccountIban = c.String(),
                        ClientAccountDescrip = c.String(),
                        ClientAccountBranchCode = c.String(),
                        ClientAddressFact = c.String(),
                        InterestPenaltyInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrincipalPenaltyInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverdueInterestInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AccruedInterestInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OverduePrincipalInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentPrincipalInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrincipalInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LoanAmountInGel = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ResponsibleUser = c.String(),
                        LocalUserID = c.Int(nullable: false),
                        TaxOrderNumber = c.Int(nullable: false),
                        Comment = c.String(),
                        DeptID = c.Int(nullable: false),
                        OrderID = c.Long(),
                    })
                .PrimaryKey(t => t.DailyPaymentID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        AltasoftUserID = c.Int(nullable: false),
                        Username = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        LastName = c.String(),
                        PrivateNumber = c.String(),
                        IsLockedOut = c.Boolean(nullable: false),
                        CanSubmit = c.Boolean(nullable: false),
                        CanViewDaily = c.Boolean(nullable: false),
                        DeptID = c.Int(nullable: false),
                        Filter_FilterID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Filters", t => t.Filter_FilterID)
                .Index(t => t.Filter_FilterID);
            
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
            
            CreateTable(
                "dbo.FilterDatas",
                c => new
                    {
                        FilterInfoID = c.Int(nullable: false, identity: true),
                        ClientID = c.Int(nullable: false),
                        DeptID = c.Int(nullable: false),
                        OperatorID = c.Int(nullable: false),
                        Filter_FilterID = c.Int(),
                    })
                .PrimaryKey(t => t.FilterInfoID)
                .ForeignKey("dbo.Filters", t => t.Filter_FilterID)
                .Index(t => t.Filter_FilterID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                        RoleDescription = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.FormWindows",
                c => new
                    {
                        FormID = c.Int(nullable: false, identity: true),
                        FormName = c.String(),
                        Role_RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.FormID)
                .ForeignKey("dbo.Roles", t => t.Role_RoleID)
                .Index(t => t.Role_RoleID);
            
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        ExceptionLogID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        IsInner = c.Boolean(nullable: false),
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
                        SignLogID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        InternalIP = c.String(),
                        ExternalIP = c.String(),
                        InternalUsername = c.String(),
                        ExternalUsername = c.String(),
                        SignType = c.Int(nullable: false),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.SignLogID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_RoleID = c.Int(nullable: false),
                        User_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleID, t.User_UserID })
                .ForeignKey("dbo.Roles", t => t.Role_RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserID, cascadeDelete: true)
                .Index(t => t.Role_RoleID)
                .Index(t => t.User_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SignLogs", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.OrderLogs", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.OrderLogs", "LocalPayment_DailyPaymentID", "dbo.DailyPayments");
            DropForeignKey("dbo.ExceptionLogs", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.ExceptionLogs", "InnerException_ExceptionLogID", "dbo.ExceptionLogs");
            DropForeignKey("dbo.CommentLogs", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_UserID", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_RoleID", "dbo.Roles");
            DropForeignKey("dbo.FormWindows", "Role_RoleID", "dbo.Roles");
            DropForeignKey("dbo.Users", "Filter_FilterID", "dbo.Filters");
            DropForeignKey("dbo.FilterDatas", "Filter_FilterID", "dbo.Filters");
            DropForeignKey("dbo.CommentLogs", "LocalPayment_DailyPaymentID", "dbo.DailyPayments");
            DropIndex("dbo.RoleUsers", new[] { "User_UserID" });
            DropIndex("dbo.RoleUsers", new[] { "Role_RoleID" });
            DropIndex("dbo.SignLogs", new[] { "User_UserID" });
            DropIndex("dbo.OrderLogs", new[] { "User_UserID" });
            DropIndex("dbo.OrderLogs", new[] { "LocalPayment_DailyPaymentID" });
            DropIndex("dbo.ExceptionLogs", new[] { "User_UserID" });
            DropIndex("dbo.ExceptionLogs", new[] { "InnerException_ExceptionLogID" });
            DropIndex("dbo.FormWindows", new[] { "Role_RoleID" });
            DropIndex("dbo.FilterDatas", new[] { "Filter_FilterID" });
            DropIndex("dbo.Users", new[] { "Filter_FilterID" });
            DropIndex("dbo.CommentLogs", new[] { "User_UserID" });
            DropIndex("dbo.CommentLogs", new[] { "LocalPayment_DailyPaymentID" });
            DropTable("dbo.RoleUsers");
            DropTable("dbo.SignLogs");
            DropTable("dbo.OrderLogs");
            DropTable("dbo.ExceptionLogs");
            DropTable("dbo.FormWindows");
            DropTable("dbo.Roles");
            DropTable("dbo.FilterDatas");
            DropTable("dbo.Filters");
            DropTable("dbo.Users");
            DropTable("dbo.DailyPayments");
            DropTable("dbo.CommentLogs");
        }
    }
}
