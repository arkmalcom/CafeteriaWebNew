namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TandaLista1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Empleadoes", "Tanda", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Empleadoes", "Tanda");
        }
    }
}
