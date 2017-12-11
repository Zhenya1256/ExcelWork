using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithExcel.Model.Entity.DalEntity
{

    [Table("Category")]
    public class Category
    {
        public Category()
        {
            ImageDictionaries = new HashSet<ImageDictionary>();

        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [StringLength(50)]
        public string ImgPath { get; set; }
        public virtual ICollection<ImageDictionary> ImageDictionaries { get; set; }

    }
}
