

namespace Application.Service.Interfaces
{
    using Application.Common;
    using Application.Model.Request;
    using System.Threading.Tasks;
    public interface IBlockingService
    {
        Task<Result> BlockUserAsync(int userId, BlockUserReq request);
        Task<Result> BlockedUserUpdateAsync(int userId, BlockUserReq request);
        Task<Result> BlockRemoveAsync(int userId, BlockUserReq request);
    }
}
