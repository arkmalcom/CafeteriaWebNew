namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TandaLista3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Empleadoes", "Tanda", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Empleadoes", "Tanda", c => c.String());
        }
    }
}
