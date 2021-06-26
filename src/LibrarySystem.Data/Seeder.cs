using AutoBogus;
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
            using (var scope = host.Services.CreateScope())
            {
                var ctx = scope.ServiceProvider.GetRequiredService<LibrarySystemDbContext>();
                ctx.Database.EnsureCreated();
                if (!ctx.Users.Any())
                {
                    var userFaker = new Faker<User>();
                    var userContactFaker = new Faker<UserContact>();
                    for (var i = 0; i < 9; i++)
                    {
                        var user = AutoFaker.Generate<User>(cfg =>
                        {
                            cfg.WithSkip<User>(nameof(User.IsEnabled));
                            cfg.WithSkip<User>(nameof(User.RentEvents));
                            cfg.WithSkip<User>(nameof(User.UserContacts));
                        });
                        var userContact1 = AutoFaker.Generate<UserContact>(cfg =>
                        {
                            cfg.WithSkip<UserContact>(nameof(UserContact.User));
                            cfg.WithSkip<UserContact>(nameof(UserContact.UserId));
                        });
                        var userContact2 = AutoFaker.Generate<UserContact>(cfg =>
                        {
                            cfg.WithSkip<UserContact>(nameof(UserContact.User));
                            cfg.WithSkip<UserContact>(nameof(UserContact.UserId));
                        });
                        var userContact3 = AutoFaker.Generate<UserContact>(cfg =>
                        {
                            cfg.WithSkip<UserContact>(nameof(UserContact.User));
                            cfg.WithSkip<UserContact>(nameof(UserContact.UserId));
                        });
                        user.AddContact(userContact1);
                        user.AddContact(userContact2);
                        user.AddContact(userContact3);
                        ctx.Users.Add(user);
                    }
                    ctx.SaveChanges();
                }
            }

            return host;
        }
    }
}
