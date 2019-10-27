namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Facturas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facturas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmpleadoId = c.Int(nullable: false),
                        ArticuloId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        FechaVenta = c.DateTime(nullable: false),
                        Monto = c.Double(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        Comentario = c.String(),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Articuloes", t => t.ArticuloId, cascadeDelete: true)
                .ForeignKey("dbo.Empleadoes", t => t.EmpleadoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.EmpleadoId)
                .Index(t => t.ArticuloId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facturas", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Facturas", "EmpleadoId", "dbo.Empleadoes");
            DropForeignKey("dbo.Facturas", "ArticuloId", "dbo.Articuloes");
            DropIndex("dbo.Facturas", new[] { "UsuarioId" });
            DropIndex("dbo.Facturas", new[] { "ArticuloId" });
            DropIndex("dbo.Facturas", new[] { "EmpleadoId" });
            DropTable("dbo.Facturas");
        }
    }
}
