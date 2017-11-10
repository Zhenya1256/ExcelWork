using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Entity;

namespace WorkWithExcel.Model.Entity
{
   public class BaseEntity : IBaseEntity
    {
        public string Value { get; set; }
    }
}
