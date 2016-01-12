namespace SenpaiMarketplace.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clozes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Inserts = c.String(),
                        Hints = c.String(),
                        LessonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonID, cascadeDelete: true)
                .Index(t => t.LessonID);
            
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Kanjis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sign = c.String(),
                        Meaning = c.String(),
                        Onyomi = c.String(),
                        Kunyomi = c.String(),
                        Example = c.String(),
                        StrokeOrder = c.String(),
                        LessonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonID, cascadeDelete: true)
                .Index(t => t.LessonID);
            
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Kana = c.String(),
                        Kanji = c.String(),
                        Translation = c.String(),
                        Description = c.String(),
                        Type = c.Int(nullable: false),
                        LessonID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lessons", t => t.LessonID, cascadeDelete: true)
                .Index(t => t.LessonID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Words", "LessonID", "dbo.Lessons");
            DropForeignKey("dbo.Kanjis", "LessonID", "dbo.Lessons");
            DropForeignKey("dbo.Clozes", "LessonID", "dbo.Lessons");
            DropIndex("dbo.Words", new[] { "LessonID" });
            DropIndex("dbo.Kanjis", new[] { "LessonID" });
            DropIndex("dbo.Clozes", new[] { "LessonID" });
            DropTable("dbo.Words");
            DropTable("dbo.Kanjis");
            DropTable("dbo.Lessons");
            DropTable("dbo.Clozes");
        }
    }
}
