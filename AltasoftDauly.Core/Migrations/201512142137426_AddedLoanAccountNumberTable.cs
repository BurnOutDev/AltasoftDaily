namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoanAccountNumberTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LoanAccountNumbers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        LoanID = c.Int(nullable: false),
                        AccountNumber = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LoanAccountNumbers");
        }
    }
}
