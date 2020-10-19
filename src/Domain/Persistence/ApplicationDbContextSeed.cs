using Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Extensions;

namespace Domain.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            // Seed, if necessary
            if (!context.Users.Any())
            {
                var users = new List<User> {
                    new User {
                                //Id = 1,
                                Name = "Selman",
                                Surname = "Ekici",
                                EmailAddress = "see@seecorp.com",
                                Password = "123".Encrypt(),
                                Username = "see"
                             },
                    new User {
                                //Id = 2,
                                Name = "Hidayet",
                                Surname = "Ekici",
                                EmailAddress = "hide@seecorp.com",
                                Password = "123".Encrypt(),
                                Username = "hide"
                              }
                    };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }

            if (!context.Chats.Any())
            {
                var chat = new Chat
                {
                    Id = Guid.Parse("{5FEA039C-57A6-455E-B5D6-E06B05BA75CD}"),
                    UserId = 2,
                    ChatType = Enums.ChatType.Private,
                    StartDate = DateTime.Now
                };

                await context.Chats.AddAsync(chat);
                await context.SaveChangesAsync();
            }

            if (!context.Messages.Any())
            {
                var messages = new List<Message>
                {
                    new Message
                    {
                        ChatId = Guid.Parse("{5FEA039C-57A6-455E-B5D6-E06B05BA75CD}"),
                        SenderUserId = 2,
                        SenderName = "Hidayet Ekici",
                        SenderEmailAddress = "hide@seecorp.com",
                        SenderUsername = "hide",
                        ReceiverName = "Selman Ekici",
                        ReceiverEmailAddress = "see@seecorp.com",
                        ReceiverUsername = "see",
                        ReceiverUserId = 1,
                        Text = "Selam SEE"
                    },
                    new Message
                    {
                        ChatId = Guid.Parse("{5FEA039C-57A6-455E-B5D6-E06B05BA75CD}"),
                        SenderUserId = 2,
                        SenderName = "Hidayet Ekici",
                        SenderEmailAddress = "hide@seecorp.com",
                        SenderUsername = "hide",
                        ReceiverName = "Selman Ekici",
                        ReceiverEmailAddress = "see@seecorp.com",
                        ReceiverUsername = "see",
                        ReceiverUserId = 1,
                        Text = "Nasilsin iyimisin"
                    }
                };

                await context.Messages.AddRangeAsync(messages);
                await context.SaveChangesAsync();
            }
        }
    }
}
