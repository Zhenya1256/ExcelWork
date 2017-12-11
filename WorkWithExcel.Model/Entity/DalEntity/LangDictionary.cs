using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithExcel.Model.Entity.DalEntity
{
    [Table("LangDictionary")]
    public class LangDictionary
    {
        [Key]
        [Column("Id")]
        public string Id { get; set; }
        public string LongName { get; set; }
    }
}