namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarios : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Cedula = c.String(nullable: false, maxLength: 13),
                        Tipo_UsuarioId = c.String(maxLength: 128),
                        FechaRegistro = c.DateTime(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetRoles", t => t.Tipo_UsuarioId)
                .Index(t => t.Tipo_UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuarios", "Tipo_UsuarioId", "dbo.AspNetRoles");
            DropIndex("dbo.Usuarios", new[] { "Tipo_UsuarioId" });
            DropTable("dbo.Usuarios");
        }
    }
}
