using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithExcel.DAL.Entity
{
    public class CategoryTranslation
    {
        public CategoryTranslation()
        {
            ImageDictionaries = new HashSet<ImageDictionary>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        public int CategoryId { get; set; }
        public int LangDictionaryId { get; set; }
        [StringLength(150)]
        public string CategoryName { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [ForeignKey("LangDictionaryId")]
        public virtual LangDictionary LangDictionary { get; set; }
        public virtual ICollection<ImageDictionary> ImageDictionaries { get; set; }

    }
}