using Bogus;
using LibrarySystem.Core.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

namespace LibrarySystem.Data
{
    public static class Seeder
    {
        public static IHost Seed(this IHost host)
        {
            var ctx = host.Services.GetRequiredService<LibrarySystemDbContext>();
            ctx.Database.EnsureCreated();
            if (!ctx.Users.Any())
            {
                var userFaker = new Faker<User>();
                var userContactFaker = new Faker<UserContact>();
                for (var i = 0; i < 9; i++)
                {
                    var user = userFaker.Generate();
                    var userContact1 = userContactFaker.Generate();
                    var userContact2 = userContactFaker.Generate();
                    var userContact3 = userContactFaker.Generate();
                    user.AddContact(userContact1);
                    user.AddContact(userContact2);
                    user.AddContact(userContact3);
                    ctx.Users.Add(user);
                }
                ctx.SaveChanges();
            }

            return host;
        }
    }
}
