using AutoMapper;
using CollectionMarket_API.Contracts;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public UserService(SignInManager<User> signInManager, 
            UserManager<User> userManager,
            IConfiguration config,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
        }

        public async Task<SignInResult> SignIn(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);
            return result;
        }


        public async Task<string> GenerateJSONWebToken(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(r => new Claim(ClaimsIdentity.DefaultRoleClaimType, r)));

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials:credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> Register(UserRegisterDTO userRegisterDTO)
        {
            var user = _mapper.Map<User>(userRegisterDTO);
            var result = await _userManager.CreateAsync(user, userRegisterDTO.Password);
            await _userManager.AddToRoleAsync(user, "Client");
            return result.Succeeded;
        }

        public async Task<bool> Exists(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return user != null;
        }

        public async Task<UserProfileDTO> GetProfileByName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            var userDto = _mapper.Map<UserProfileDTO>(user);
            return userDto;
        }

        public async Task<bool> UpdateProfile(UserProfileDTO model, string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.City = model.City;
            user.PostCode = model.PostCode;
            user.Address = model.Address;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<UserDTO> GetByName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            var userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<bool> Deposit(CashFlowDTO cashFlow, string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            user.Money += cashFlow.AmountOfMoney;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> Withdraw(CashFlowDTO cashFlow, string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user.Money < cashFlow.AmountOfMoney)
                return false;
            user.Money -= cashFlow.AmountOfMoney;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<decimal> GetMoney(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return user.Money;
        }

        public bool HasEnoughMoney(User buyer, decimal price)
        {
            return buyer.Money >= price;
        }

        public async Task<bool> SendMoney(Order order)
        {
            var buyer = order.Buyer;
            var seller = order.SaleOffers.FirstOrDefault().Seller;
            buyer.Money -= order.Price;
            seller.Money += order.Price;
            var result = await _userManager.UpdateAsync(buyer);
            if (result.Succeeded)
            {
                result = await _userManager.UpdateAsync(seller);
                return result.Succeeded;
            }
            return false;
        }
    }
}
