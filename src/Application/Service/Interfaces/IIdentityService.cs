using Application.Common;
using Application.Model.Request;
using Application.Model.Response;
using System.Threading.Tasks;

namespace Application.Service.Interfaces
{
    public interface IIdentityService
    {
        Task<ResultExtended<UserRes>> RegisterAsync(UserReq userReq);
        Task<ResultExtended<LoginRes>> LoginAsync(LoginReq loginReq);
    }
}
