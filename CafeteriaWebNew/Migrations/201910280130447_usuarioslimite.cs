namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarioslimite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "LimiteCredito", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "LimiteCredito");
        }
    }
}
