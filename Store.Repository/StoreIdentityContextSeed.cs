using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Shimaaa Nabil",
                    Email = "Shimaa@Gmail.com",
                    UserName="Shimaaaa_2003",
                    Address = new Address
                    {
                        FirstName = "Shimaa",
                        LastName = "Nabil",
                        City = "Maadi",
                        State = "Cairo",
                        Street = "105",
                        PostalCode = "123456"
                    }
                };
                await userManager.CreateAsync(user, "Password123!");
            }
        }
    }
}
