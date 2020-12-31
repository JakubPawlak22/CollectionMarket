using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts
{
    public interface IUserService
    {
        Task<SignInResult> SignIn(string username, string password);
        Task<string> GenerateJSONWebToken(string username);
    }
}
