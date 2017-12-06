using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithExcel.DAL.Entity
{
    

    [Table("ImageDictionary")]
    public class ImageDictionary
    {
        public ImageDictionary()
        {
            ImageDescriptions = new HashSet<ImageDescription>();
            //ImageDictionaryData = new HashSet<ImageDictionaryData>();
            //ImagePositions = new HashSet<ImagePosition>();
            //UserFreeImages = new HashSet<UserFreeImage>();
            //UserPayments = new HashSet<UserPayment>();
            //ChildItems = new HashSet<ImageDictionary>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        public int? CategoryTranslationId { get; set; }

        public int? RootImageId { get; set; }

        public bool? DisplayAtProgram { get; set; }
        [ForeignKey("CategoryTranslationId")]
        public virtual CategoryTranslation CategoryTranslation { get; set; }

        public virtual ICollection<ImageDescription> ImageDescriptions { get; set; }

        //public virtual ICollection<ImageDictionaryData> ImageDictionaryData { get; set; }

        //public virtual ICollection<ImagePosition> ImagePositions { get; set; }

        //[ForeignKey("RootImageId")]
        //public virtual ImageDictionary RootImage { get; set; }
        ////public virtual ICollection<ImageDictionary> ChildItems { get; set; }
        //public virtual ICollection<UserFreeImage> UserFreeImages { get; set; }

        //public virtual ICollection<UserPayment> UserPayments { get; set; }
    }
}
