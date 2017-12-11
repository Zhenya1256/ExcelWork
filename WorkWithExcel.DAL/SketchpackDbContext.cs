using System.Data.Entity;
using WorkWithExcel.Model.Entity.DalEntity;

namespace WorkWithExcel.DAL
{
    public class SketchpackDbContext : DbContext
    {
        public SketchpackDbContext() : base("SketchpackDbContext")
        {
        }
        public virtual DbSet<Category> Category { get; set; }
        //public virtual DbSet<Description> Descriptions { get; set; }
        public virtual DbSet<ImageDescription> ImageDescriptions { get; set; }
        public virtual DbSet<CategoryTranslation> CategoryTranslation { get; set; }
        public virtual DbSet<ImageDictionary> ImageDictionary { get; set; }
        public virtual DbSet<LangDictionary> LangDictionary { get; set; }


    }
}