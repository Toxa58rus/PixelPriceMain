using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Mail
{
    public class MailRequest
    {
        /// <summary>
        /// Email отправителя
        /// </summary>
        public string From { get; set; }
        
        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
        
        /// <summary>
        /// Email получателя
        /// </summary>
        public string To { get; set; }
        
        /// <summary>
        /// Email для ответа
        /// </summary>
        public string Reply { get; set; }
        
        /// <summary>
        /// HTML-версия письма
        /// </summary>
        public string Html { get; set; }
        
        /// <summary>
        /// TXT-версия письма
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дополнительные заголовки [{ "X-Name1": “value1” }, { "X-Name2": “value2” }, …]
        /// </summary>
        public string Headers { get; set; }

    }
}
