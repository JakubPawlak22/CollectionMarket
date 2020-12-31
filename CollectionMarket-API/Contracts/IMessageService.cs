using CollectionMarket_API.DTOs;
using CollectionMarket_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts
{
    public interface IMessageService
    {
        Task<IList<MessageDTO>> GetConversation(MessageFiltersDTO filtersDTO);
        Task<CreateObjectResult> Create(MessageCreateDTO messageDTO);
    }
}
