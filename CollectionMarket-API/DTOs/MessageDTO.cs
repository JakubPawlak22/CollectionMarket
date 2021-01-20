﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class MessageDTO
    {
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    } 
    public class MessageCreateDTO
    {
        public string ReceiverUsername { get; set; }
        public string Content { get; set; }
    }

    public class MessageFiltersDTO
    {
        public string FirstUserId { get; set; }
        public string SecondUserId { get; set; }
    }
}
