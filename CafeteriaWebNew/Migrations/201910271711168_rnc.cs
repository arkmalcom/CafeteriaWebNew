namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rnc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Proveedors", "RNC", c => c.String(nullable: false, maxLength: 9));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Proveedors", "RNC", c => c.String(nullable: false));
        }
    }
}
