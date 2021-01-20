using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Models
{
        public class MessageModel
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
    public class MessageCreateModel
    {
        public string ReceiverUsername { get; set; }
        public string Content { get; set; }
    }
}
