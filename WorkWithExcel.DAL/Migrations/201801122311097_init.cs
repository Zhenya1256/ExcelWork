namespace WorkWithExcel.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImgPath = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImageDictionary",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(),
                        RootImageId = c.Int(),
                        SexType = c.String(),
                        DisplayAtProgram = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.ImageDictionary", t => t.RootImageId)
                .Index(t => t.CategoryId)
                .Index(t => t.RootImageId);
            
            CreateTable(
                "dbo.ImageDescription",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsMainTranslation = c.Boolean(nullable: false),
                        ImageDictionaryId = c.Int(nullable: false),
                        Description = c.String(),
                        LangDictionaryId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ImageDictionary", t => t.ImageDictionaryId, cascadeDelete: true)
                .ForeignKey("dbo.LangDictionary", t => t.LangDictionaryId)
                .Index(t => t.ImageDictionaryId)
                .Index(t => t.LangDictionaryId);
            
            CreateTable(
                "dbo.LangDictionary",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        LongName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CategoryTranslation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsMainTranslation = c.Boolean(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        LangDictionaryId = c.String(maxLength: 128),
                        CategoryName = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.LangDictionary", t => t.LangDictionaryId)
                .Index(t => t.CategoryId)
                .Index(t => t.LangDictionaryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryTranslation", "LangDictionaryId", "dbo.LangDictionary");
            DropForeignKey("dbo.CategoryTranslation", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.ImageDictionary", "RootImageId", "dbo.ImageDictionary");
            DropForeignKey("dbo.ImageDescription", "LangDictionaryId", "dbo.LangDictionary");
            DropForeignKey("dbo.ImageDescription", "ImageDictionaryId", "dbo.ImageDictionary");
            DropForeignKey("dbo.ImageDictionary", "CategoryId", "dbo.Category");
            DropIndex("dbo.CategoryTranslation", new[] { "LangDictionaryId" });
            DropIndex("dbo.CategoryTranslation", new[] { "CategoryId" });
            DropIndex("dbo.ImageDescription", new[] { "LangDictionaryId" });
            DropIndex("dbo.ImageDescription", new[] { "ImageDictionaryId" });
            DropIndex("dbo.ImageDictionary", new[] { "RootImageId" });
            DropIndex("dbo.ImageDictionary", new[] { "CategoryId" });
            DropTable("dbo.CategoryTranslation");
            DropTable("dbo.LangDictionary");
            DropTable("dbo.ImageDescription");
            DropTable("dbo.ImageDictionary");
            DropTable("dbo.Category");
        }
    }
}
