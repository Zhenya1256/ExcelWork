using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithExcel.Model.Entity.DalEntity
{
    [Table("ImageDictionary")]
    public class ImageDictionary
    {
        public ImageDictionary()
        {
            ImageDescriptions = new HashSet<ImageDescription>();
            //ChildItems = new HashSet<ImageDictionary>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        public int? CategoryId { get; set; }

        public int? RootImageId { get; set; }
        public string SexType { get; set; }
        public bool? DisplayAtProgram { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<ImageDescription> ImageDescriptions { get; set; }
        

        [ForeignKey("RootImageId")]
        public virtual ImageDictionary RootImage { get; set; }
    }
}
