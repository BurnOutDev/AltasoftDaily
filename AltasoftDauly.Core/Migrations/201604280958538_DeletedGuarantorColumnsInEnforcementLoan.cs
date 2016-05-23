namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedGuarantorColumnsInEnforcementLoan : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.EnforcementLoans", "GuarantorName");
            DropColumn("dbo.EnforcementLoans", "GuarantorPrivateNumber");
            DropColumn("dbo.EnforcementLoans", "GuarantorPhone");
            DropColumn("dbo.EnforcementLoans", "GuarantorAddress");
            DropColumn("dbo.EnforcementLoans", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.EnforcementLoans", "ID", c => c.String());
            AddColumn("dbo.EnforcementLoans", "GuarantorAddress", c => c.String());
            AddColumn("dbo.EnforcementLoans", "GuarantorPhone", c => c.String());
            AddColumn("dbo.EnforcementLoans", "GuarantorPrivateNumber", c => c.String());
            AddColumn("dbo.EnforcementLoans", "GuarantorName", c => c.String());
        }
    }
}
