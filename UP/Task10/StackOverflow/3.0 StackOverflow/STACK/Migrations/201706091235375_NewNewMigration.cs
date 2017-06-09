namespace STACK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewNewMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "Rating");
        }
    }
}
