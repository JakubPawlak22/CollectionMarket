using AutoMapper;
using CollectionMarket_API.Contracts;
using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using CollectionMarket_API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
        }

        public async Task<Tuple<int,bool>> Create(MessageCreateDTO messageDTO)
        {
            var message = _mapper.Map<Message>(messageDTO);
            var isSuccess = await _messageRepository.Create(message);
            return new Tuple<int, bool>(message.Id, isSuccess);
        }

        public async Task<IList<MessageDTO>> GetConversation(MessageFiltersDTO filtersDTO)
        {
            var filters = _mapper.Map<MessageFilters>(filtersDTO);
            var messages = await _messageRepository.GetFiltered(filters);
            var messageDTOs = _mapper.Map<IList<MessageDTO>>(messages);
            return messageDTOs;
        }
    }
}
