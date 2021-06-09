using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models.Chat
{
    public class EditMessageResponseModel
    {
        public string MessageId { get; set; }
        public string NewText { get; set; }
    }
}
