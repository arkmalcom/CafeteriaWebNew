namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class articulos3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articuloes", "ProveedorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Articuloes", "ProveedorId");
            AddForeignKey("dbo.Articuloes", "ProveedorId", "dbo.Proveedors", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articuloes", "ProveedorId", "dbo.Proveedors");
            DropIndex("dbo.Articuloes", new[] { "ProveedorId" });
            DropColumn("dbo.Articuloes", "ProveedorId");
        }
    }
}
