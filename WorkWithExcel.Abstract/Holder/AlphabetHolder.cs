using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithExcel.Abstract.Holder
{
    public static class AlphabetHolder
    {
        public static Dictionary<int, string> _dictionary =
             new Dictionary<int, string>();

        static AlphabetHolder()
        {
            Init();
        }

        private static void Init()
        {
            int i = 1;
            for (char a = 'A'; a < 'Z'; a++)
            {
                _dictionary.Add(i, a.ToString());
                i++;
            }
        }

        public static string GetSmbol(int nomer)
        {
            if (_dictionary.ContainsKey(nomer))
            {
                return _dictionary[nomer];
            }

            return null;
        }
    }
}
