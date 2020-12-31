using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class MessageDTO
    {
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    } 
    public class MessageCreateDTO
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }

    public class MessageFiltersDTO
    {
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }
    }
}
