namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campus2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cafeterias", "CampusId", c => c.Int(nullable: false));
            CreateIndex("dbo.Cafeterias", "CampusId");
            AddForeignKey("dbo.Cafeterias", "CampusId", "dbo.Campus", "ID", cascadeDelete: true);
            DropColumn("dbo.Cafeterias", "Campus_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cafeterias", "Campus_id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Cafeterias", "CampusId", "dbo.Campus");
            DropIndex("dbo.Cafeterias", new[] { "CampusId" });
            DropColumn("dbo.Cafeterias", "CampusId");
        }
    }
}
