﻿using WorkWithExcel.Abstract.Common;

namespace WorkWithExcel.Model.Common
{
   public class DataResult<T> : IDataResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
