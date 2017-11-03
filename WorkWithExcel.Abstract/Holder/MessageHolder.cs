using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkWithExcel.Abstract.Enums;

namespace WorkWithExcel.Abstract.Holder
{
   public static class MessageHolder
    {
        private  static readonly Dictionary<MessageType,string> _messagesDictionary = 
            new Dictionary<MessageType, string>();

        static MessageHolder()
        {
            InitMessage();
        }

        private static void  InitMessage()
        {
            _messagesDictionary.Add(MessageType.FileIsempty, "Файл не выбран");
            _messagesDictionary.Add(MessageType.NotFormat, "Выберите файл с расширением xlsx");
            _messagesDictionary.Add(MessageType.NotSexType, "Пуста ячийка 'sex/пол' рядок -");
            _messagesDictionary.Add(MessageType.NotNameTitle, "Відсутня назва заголовка стовпчика -");
            _messagesDictionary.Add(MessageType.NotTranslate, "Відсутній перевод рядка-");
            _messagesDictionary.Add(MessageType.AlreadyAddLanguage, "зустрічаються дві однакові мови - ");
            //NotTranslate
        }

        public static string GetErrorMessage(MessageType type)
        {
            string message=String.Empty;
            
            if (_messagesDictionary.ContainsKey(type))
            {
                message = _messagesDictionary[type];
            }

            return message;
        }

    }
}
