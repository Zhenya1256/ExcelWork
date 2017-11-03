﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Entity;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.BL.Entity
{
   public class TranslateEntity : ITranslateEntity
    {
        public string Index { get; set; }
        public string PageNomer { get; set; }
        public SexType SexType { get; set; }
        public Dictionary<string, string> TranslateDictionary { get; set; }
    }
}