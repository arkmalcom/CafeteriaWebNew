namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tiposusuario : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "Estado", c => c.Boolean());
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.AspNetRoles", "Estado");
        }
    }
}
