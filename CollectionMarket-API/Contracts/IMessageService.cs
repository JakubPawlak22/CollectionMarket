using CollectionMarket_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts
{
    public interface IMessageService
    {
        Task<IList<MessageDTO>> GetAll();
        Task<Tuple<int, bool>> Create(MessageCreateDTO messageDTO);
    }
}
