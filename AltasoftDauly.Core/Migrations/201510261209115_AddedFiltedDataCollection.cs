namespace AltasoftDaily.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFiltedDataCollection : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FilterDatas", "Filter_FilterID", "dbo.Filters");
            DropIndex("dbo.FilterDatas", new[] { "Filter_FilterID" });
            DropTable("dbo.FilterDatas");
        }
    }
}
