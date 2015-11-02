namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommentProperty_DailyPayment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyPayments", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyPayments", "Comment");
        }
    }
}
