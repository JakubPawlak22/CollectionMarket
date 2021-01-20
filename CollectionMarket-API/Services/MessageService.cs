using AutoMapper;
using CollectionMarket_API.Contracts;
using CollectionMarket_API.Contracts.Repositories;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using CollectionMarket_API.Filters;
using CollectionMarket_API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services
{
    public class MessageService : IMessageService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CreateObjectResult> Create(MessageCreateDTO messageDTO, string username)
        {
            var sender = await _userManager.FindByNameAsync(username);
            var receiver = await _userManager.FindByNameAsync(messageDTO.ReceiverUsername);
            var message = new Message
            {
                Sender = sender,
                Receiver = receiver,
                Content = messageDTO.Content,
                Date = DateTime.Now,
                IsRead = false
            };
            var isSuccess = await _messageRepository.Create(message);
            return new CreateObjectResult(isSuccess, message.Id);
        }

        public async Task<IList<MessageDTO>> GetConversation(string firstUserName, string secondUserName)
        {
            var filters = new MessageFilters()
            {
                FirstUserName = firstUserName,
                SecondUserName = secondUserName
            };
            var messages = await _messageRepository.GetFiltered(filters);
            var messageDTOs = _mapper.Map<IList<MessageDTO>>(messages);
            return messageDTOs;
        }

        public async Task<IList<ConversationDTO>> GetConversations(string loggedUserName)
        {
            var lastMessages = await _messageRepository.GetLastMessages(loggedUserName);
            var dtos = lastMessages.Select(x => new ConversationDTO
            {
                Date = x.Date,
                LastMessageContent = x.Content,
                Username = x.Sender.UserName.Equals(loggedUserName) ?
                    x.Receiver.UserName : x.Sender.UserName
            }).ToList();
            return dtos;
        }
    }
}
