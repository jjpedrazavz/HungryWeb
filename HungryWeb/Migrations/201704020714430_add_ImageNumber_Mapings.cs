namespace HungryWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_ImageNumber_Mapings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FoodImageMapping", "ImageNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Alimentos", "FoodImageMappingID", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Alimentos", "FoodImageMappingID", c => c.Int(nullable: false));
            DropColumn("dbo.FoodImageMapping", "ImageNumber");
        }
    }
}
