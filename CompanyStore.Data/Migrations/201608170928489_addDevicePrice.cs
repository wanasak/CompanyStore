namespace CompanyStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDevicePrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Device", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Device", "Price");
        }
    }
}
