using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts
{
    public interface IMessageRepository
    {
        Task<IList<MessageModel>> GetConversation(string url, string username);
        Task<bool> Create(string url, MessageCreateModel model);
        Task<IList<ConversationModel>> GetConversations(string url);
    }
}
