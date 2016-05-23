namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProblemManagerIDToEnforcementLoans : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnforcementLoans", "ProblemManagerID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EnforcementLoans", "ProblemManagerID");
        }
    }
}
