namespace CafeteriaWebNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TandaLista : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Empleadoes", "Tanda");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Empleadoes", "Tanda", c => c.String());
        }
    }
}
