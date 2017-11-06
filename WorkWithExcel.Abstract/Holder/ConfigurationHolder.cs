using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleConfig;
using WorkWithExcel.Abstract.Common.Config;

namespace WorkWithExcel.Abstract.Holder
{
   public static  class ConfigurationHolder
   {
       private static  ExcelConfiguration _exelConfiguration;

        public static ExcelConfiguration ApiConfiguration
        {
            get
            {
                if (_exelConfiguration == null)
                {

                    lock (typeof(ConfigurationHolder))
                    {
                        if (_exelConfiguration == null)
                        {

                            _exelConfiguration = Configuration.
                                Load<ExcelConfiguration>("ExcelConfiguration");
                        }
                    }
                }

                return _exelConfiguration;
            }
        }
    }
}
