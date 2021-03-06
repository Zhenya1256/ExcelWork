﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkWithExcel.Model.Entity.DalEntity
{
    [Table("CategoryTranslation")]

    public class CategoryTranslation
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        public bool IsMainTranslation { get;set; }
        public int CategoryId { get; set; }
        public string LangDictionaryId { get; set; }
        [StringLength(150)]
        public string CategoryName { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [ForeignKey("LangDictionaryId")]
        public virtual LangDictionary LangDictionary { get; set; }
    }
}