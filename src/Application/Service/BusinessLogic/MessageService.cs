using Application.Common;
using Application.Model.Response;
using Application.Service.Interfaces;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using NLog.Targets.Wrappers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.BusinessLogic
{
    public class MessageService : IMessageService
    {
        private readonly IApplicationDbContext _dbContext;

        public MessageService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultExtended<PagedResults<MessageRes>>> GetMessages(string chatId, int page, int size = 50)
        {
            if (!Guid.TryParse(chatId, out Guid parsedChatId))
                return ResultExtended<PagedResults<MessageRes>>.Failure(null, new[] { "Chat not valid any more" });


            var messages = _dbContext.Messages.Where(x => x.ChatId == parsedChatId).OrderByDescending(x => x.Id);
            var totalCount = messages.Count();
            //
            page = page < 1 ? 1 : page;
            size = size < 1 ? 50 : size;
            var pageCount = (int)Math.Ceiling((double)totalCount / size);
            var hasNextPage = pageCount > page;
            var hasPrevPage = page > 1;
            var pagedRecords = messages
                .Skip(size * (page - 1))
                .Take(size).OrderByDescending(x => x.DateOfInsert)?.Select(s => new MessageRes
                {
                    ChatId = chatId,
                    SenderUsername = s.SenderUsername,
                    ReceiverName = s.ReceiverName,
                    ReceiverUsername = s.ReceiverUsername,
                    Text = s.Text
                }).AsEnumerable();

            var data = new PagedResults<MessageRes>
            {
                Data = pagedRecords,
                PageCount = pageCount,
                Count = totalCount,
                CurrentPage = page,
                HasNextPage = hasNextPage,
                HasPrevPage = hasPrevPage,
            };

            return ResultExtended<PagedResults<MessageRes>>.Success(data);
        }

        public async Task<ResultExtended<string>> StartChat(int userId)
        {
            var chat = new Domain.Entities.Chat
            {
                UserId = userId,
                ChatType = Domain.Enums.ChatType.Private,
            };
            await _dbContext.Chats.AddAsync(chat);
            var result = await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());
            if (result > 0)
            {
                var id = chat.Id.ToString();
                return ResultExtended<string>.Success(id);
            }
            else
                return ResultExtended<string>.Failure(null, new[] { "Something went wrong" });
        }


        public async Task<Result> SendMessage(int userId, string chatId, string username, string text)
        {
            if (!Guid.TryParse(chatId, out Guid parsedChatId))
                return Result.Failure(new[] { "Chat not valid any more" });
            //
            if (!await _dbContext.Chats.AnyAsync(x => x.Id == parsedChatId))
                return Result.Failure(new[] { "There is no chat room" });
            //
            var receiverUser = await _dbContext.Users.Where(x => x.Username == username).FirstOrDefaultAsync();
            if (await _dbContext.Blockings.AnyAsync(x => x.BlockedUserId == userId && x.ObstructionistUserId == receiverUser.Id && !x.IsDeleted))
                return Result.Failure(new[] { "This user blocked you" });
            //
            var senderUser = await _dbContext.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            var message = new Message
            {
                ChatId = parsedChatId,
                SenderEmailAddress = senderUser.EmailAddress,
                SenderName = $"{senderUser.Name} {senderUser.Surname}",
                SenderUsername = senderUser.Username,
                SenderUserId = senderUser.Id,
                ReceiverName = $"{receiverUser.Name} {receiverUser.Surname}",
                ReceiverUsername = receiverUser.Username,
                ReceiverEmailAddress = receiverUser.EmailAddress,
                ReceiverUserId = receiverUser.Id,
                Text = text,
            };
            //
            await _dbContext.Messages.AddAsync(message);
            var result = await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());
            if (result > 0)
                return Result.Success(new[] { "Successful" });
            else
                return Result.Failure(new[] { "Something went wrong" });
        }
    }
}
