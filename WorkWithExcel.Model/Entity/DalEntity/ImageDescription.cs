using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithExcel.Model.Entity.DalEntity
{

    [Table("ImageDescription")]
    public class ImageDescription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public bool IsMainTranslation { get; set; }

        public int ImageDictionaryId { get; set; }

        public string Description { get; set; }

        //public virtual Description Description { get; set; }
        public string LangDictionaryId { get; set; }
        [ForeignKey("LangDictionaryId")]
        public virtual LangDictionary LangDictionary { get; set; }
        [ForeignKey("ImageDictionaryId")]
        public virtual ImageDictionary ImageDictionary { get; set; }
    }
}
