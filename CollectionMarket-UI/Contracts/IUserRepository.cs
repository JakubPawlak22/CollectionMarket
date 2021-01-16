using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts
{
    public interface IUserRepository
    {
        Task<UserProfileModel> GetLoggedUser(string url);
        Task<bool> UpdateLoggedUser(string url, UserProfileModel model);
        Task<UserModel> Get(string url, string name);
    }
}
