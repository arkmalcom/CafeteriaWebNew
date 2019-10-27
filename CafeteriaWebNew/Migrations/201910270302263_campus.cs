namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cafeterias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Campus_id = c.Int(nullable: false),
                        Encargado = c.String(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Campus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Campus");
            DropTable("dbo.Cafeterias");
        }
    }
}
