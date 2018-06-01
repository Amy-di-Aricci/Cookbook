namespace Cookbook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Annotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Recipes", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "Description", c => c.String());
            AlterColumn("dbo.Recipes", "Name", c => c.String());
        }
    }
}
