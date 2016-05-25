namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedColumnsInEnforcementAndChangeLogModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChangeLogs",
                c => new
                    {
                        ChangeLogID = c.Int(nullable: false, identity: true),
                        EntityName = c.String(),
                        EntityID = c.Int(nullable: false),
                        OldValue = c.String(),
                        NewValue = c.String(),
                        LogDate = c.DateTime(nullable: false),
                        User_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.ChangeLogID)
                .ForeignKey("dbo.Users", t => t.User_UserID)
                .Index(t => t.User_UserID);
            
            AddColumn("dbo.EnforcementLoans", "Identificator", c => c.String());
            AddColumn("dbo.EnforcementLoans", "AppPrincipal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.EnforcementLoans", "AppInterest", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.EnforcementLoans", "AppPenalty", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ChangeLogs", "User_UserID", "dbo.Users");
            DropIndex("dbo.ChangeLogs", new[] { "User_UserID" });
            DropColumn("dbo.EnforcementLoans", "AppPenalty");
            DropColumn("dbo.EnforcementLoans", "AppInterest");
            DropColumn("dbo.EnforcementLoans", "AppPrincipal");
            DropColumn("dbo.EnforcementLoans", "Identificator");
            DropTable("dbo.ChangeLogs");
        }
    }
}
