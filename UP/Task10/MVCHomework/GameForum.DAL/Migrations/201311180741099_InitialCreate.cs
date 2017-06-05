namespace GameForum.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentItems",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Body = c.String(),
                        GameKey = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.GameItems", t => t.GameKey)
                .Index(t => t.GameKey);
            
            CreateTable(
                "dbo.GameItems",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        GenreId = c.Int(nullable: false),
                        AgeRestriction = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .ForeignKey("dbo.GenreItems", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.GenreItems",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        Genre = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GameItems", "GenreId", "dbo.GenreItems");
            DropForeignKey("dbo.CommentItems", "GameKey", "dbo.GameItems");
            DropIndex("dbo.GameItems", new[] { "GenreId" });
            DropIndex("dbo.CommentItems", new[] { "GameKey" });
            DropTable("dbo.GenreItems");
            DropTable("dbo.GameItems");
            DropTable("dbo.CommentItems");
        }
    }
}
