using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class ConversationDTO
    {
        public string Username { get; set; }
        public string LastMessageContent { get; set; }
        public DateTime Date { get; set; }
    }
}
