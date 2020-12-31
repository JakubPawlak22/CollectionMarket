using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Data
{
    public static class SeedData
    {
        public async static Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }
        public async static Task SeedUsers(UserManager<User> userManager) 
        {
            if(await userManager.FindByEmailAsync("admin@asdzxc.pl")==null)
            {
                var user = new User
                {
                    UserName = "admin",
                    Email = "admin@asdzxc.pl",
                    FirstName = "ad",
                    LastName = "min"
                };
                var result = await userManager.CreateAsync(user, "@2Qwerty");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Administrator");
                }
            }
            if (await userManager.FindByEmailAsync("client@asdzxc.pl") == null)
            {
                var user = new User
                {
                    UserName = "client",
                    Email = "client@asdzxc.pl",
                    FirstName = "cli",
                    LastName = "ent"
                };
                var result = await userManager.CreateAsync(user, "@2Qwerty");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Client");
                }
            }
        }
        public async static Task SeedRoles(RoleManager<IdentityRole> roleManager) 
        {
            if(!await roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole
                {
                    Name = "Administrator"
                };
            await roleManager.CreateAsync(role);
            }
            if(!await roleManager.RoleExistsAsync("Client"))
            {
                var role = new IdentityRole
                {
                    Name = "Client"
                };
            await roleManager.CreateAsync(role);
            }
        }

    }
}
