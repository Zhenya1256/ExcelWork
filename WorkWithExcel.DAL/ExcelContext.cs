using WorkWithExcel.DAL.Entity;

namespace WorkWithExcel.DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ExcelContext : DbContext
    {

        public ExcelContext()
            : base("ExcelContext")
        {

        }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public virtual DbSet<ImageDescription> ImageDescriptions { get; set; }
        public virtual DbSet<ImageDictionary> ImageDictionarys { get; set; }
        public virtual DbSet<LangDictionary> LangDictionarys { get; set; }

    }


}