using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithExcel.DAL.Entity
{
    

    public  class Category
    {
        public Category()
        {
            
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [StringLength(50)]
        public string ImgPath { get; set; }

    }
}
