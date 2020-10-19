using Application.Common;
using Application.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface IMessageService
    {
        Task<Result> SendMessage(int userId, string chatId, string username, string text);
        Task<ResultExtended<PagedResults<MessageRes>>> GetMessages(string chatId, int page, int size = 50);
        Task<ResultExtended<string>> StartChat(int userId);
    }
}
