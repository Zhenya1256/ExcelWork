using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithExcel.DAL.Entity
{
    public class LangDictionary
    {
        //TODO : think about langId= 'en'
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        public string ShortName { get; set; }
        public string LongName { get; set; }
    }
}