namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class campus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.campus",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        descripcion = c.String(),
                        estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.cafeterias",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        descripcion = c.String(),
                        id_campus = c.Int(nullable: false),
                        encargado = c.String(),
                        estado = c.Boolean(nullable: false),
                        campus_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.campus", t => t.campus_id)
                .Index(t => t.campus_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.cafeterias", "campus_id", "dbo.campus");
            DropIndex("dbo.cafeterias", new[] { "campus_id" });
            DropTable("dbo.cafeterias");
            DropTable("dbo.campus");
        }
    }
}
