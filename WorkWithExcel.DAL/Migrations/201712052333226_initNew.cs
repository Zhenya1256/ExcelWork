namespace WorkWithExcel.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initNew : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgPath = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        LangDictionaryId = c.Int(nullable: false),
                        CategoryName = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.LangDictionaries", t => t.LangDictionaryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.LangDictionaryId);
            
            CreateTable(
                "dbo.ImageDictionary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryTranslationId = c.Int(),
                        RootImageId = c.Int(),
                        DisplayAtProgram = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryTranslations", t => t.CategoryTranslationId)
                .Index(t => t.CategoryTranslationId);
            
            CreateTable(
                "dbo.ImageDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageDictionaryId = c.Int(nullable: false),
                        Description = c.String(),
                        LangDictionaryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageDictionary", t => t.ImageDictionaryId, cascadeDelete: true)
                .ForeignKey("dbo.LangDictionaries", t => t.LangDictionaryId, cascadeDelete: true)
                .Index(t => t.ImageDictionaryId)
                .Index(t => t.LangDictionaryId);
            
            CreateTable(
                "dbo.LangDictionaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ShortName = c.String(),
                        LongName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryTranslations", "LangDictionaryId", "dbo.LangDictionaries");
            DropForeignKey("dbo.ImageDescription", "LangDictionaryId", "dbo.LangDictionaries");
            DropForeignKey("dbo.ImageDescription", "ImageDictionaryId", "dbo.ImageDictionary");
            DropForeignKey("dbo.ImageDictionary", "CategoryTranslationId", "dbo.CategoryTranslations");
            DropForeignKey("dbo.CategoryTranslations", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ImageDescription", new[] { "LangDictionaryId" });
            DropIndex("dbo.ImageDescription", new[] { "ImageDictionaryId" });
            DropIndex("dbo.ImageDictionary", new[] { "CategoryTranslationId" });
            DropIndex("dbo.CategoryTranslations", new[] { "LangDictionaryId" });
            DropIndex("dbo.CategoryTranslations", new[] { "CategoryId" });
            DropTable("dbo.LangDictionaries");
            DropTable("dbo.ImageDescription");
            DropTable("dbo.ImageDictionary");
            DropTable("dbo.CategoryTranslations");
            DropTable("dbo.Categories");
        }
    }
}
