namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class articulos : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cafeterias", "Descripcion", c => c.String(nullable: false, maxLength: 60));
            AlterColumn("dbo.Campus", "Descripcion", c => c.String(nullable: false, maxLength: 60));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Campus", "Descripcion", c => c.String());
            AlterColumn("dbo.Cafeterias", "Descripcion", c => c.String());
        }
    }
}
