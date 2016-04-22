namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCommentTypeToCommentLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentLogs", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentLogs", "Type");
        }
    }
}
