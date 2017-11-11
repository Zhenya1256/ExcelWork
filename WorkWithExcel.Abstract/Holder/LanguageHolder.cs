using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Abstract;

namespace WorkWithExcel.Abstract.Holder
{
    public static class LanguageHolder
    {
        private static Dictionary<string, string> _languageDictionary =
            new Dictionary<string, string>();

        static LanguageHolder()
        {
            Init();
        }

        private static void Init()
        {
            _languageDictionary.Add("en", "English");
            _languageDictionary.Add("zh", "Chinese");
            _languageDictionary.Add("cs", "Czech");
            _languageDictionary.Add("da", "Danish");
            _languageDictionary.Add("nl", "Dutch");
            _languageDictionary.Add("eo", "Esperanto");
            _languageDictionary.Add("fr", "French");
            _languageDictionary.Add("ka", "Georgian");
            _languageDictionary.Add("fi", "Finnish");
            _languageDictionary.Add("de", "German");
            _languageDictionary.Add("el", "Greek");
            _languageDictionary.Add("it", "Italian");
            _languageDictionary.Add("ja", "Japanese");
            _languageDictionary.Add("ko", "Korean");
            _languageDictionary.Add("ku", "Kurdish");
            _languageDictionary.Add("fa", "Persian");
            _languageDictionary.Add("pl", "Polish");
            _languageDictionary.Add("pt", "Portuguese");
            _languageDictionary.Add("ro", "Romanian");
            _languageDictionary.Add("ru", "Russian");
            _languageDictionary.Add("es", "Spanish");
            _languageDictionary.Add("sv", "Swedish");
            _languageDictionary.Add("tr", "Turkish");
            _languageDictionary.Add("ur", "Urdu");
            _languageDictionary.Add("uk", "Ukrain");
        }

        public static string GetISOCodes(string language, IDataNormalization normalization)
        {
            language = normalization.NormalizeString(language).Data;
            List<string> tempValues = _languageDictionary.Values.Select
                (p => p = normalization.NormalizeString(p).Data).ToList();
            if (
                !tempValues.Contains(language)
                )
            {
                return language;
            }

            return _languageDictionary.FirstOrDefault(p =>
                language.Contains(normalization.NormalizeString(p.Value).Data)).Key;
        }

        public static string GetLanguage(string code)
        {
            if (!_languageDictionary.Keys.Contains(code))
            {
                return null;
            }

            return _languageDictionary[code];
        }
    }
}
