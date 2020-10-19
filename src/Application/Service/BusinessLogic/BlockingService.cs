using Application.Common;
using Application.Model.Request;
using Application.Service.Interfaces;
using Domain.Entities;
using Domain.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Service.BusinessLogic
{
    public class BlockingService : IBlockingService
    {
        private readonly IApplicationDbContext _dbContext;

        public BlockingService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result> BlockUserAsync(int userId, BlockUserReq request)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.Id == userId))
                return Result.Failure(new[] { "The user must be delete from system. Pls contact to the manager." });
            //
            var userWillBlock = await _dbContext.Users.Where(x => x.Username == request.Username).FirstOrDefaultAsync();
            if (userWillBlock == null)
                return Result.Failure(new[] { "The user to be blocked does not exist in the system" });

            var blockedUser = new Blocking
            {
                BlockedUserId = userWillBlock.Id,
                ObstructionistUserId = userId,
                Status = true
            };
            _dbContext.Blockings.Add(blockedUser);
            var result = await _dbContext.SaveChangesAsync(new CancellationToken());
            if (result > 0)
                return Result.Success(new[] { "Successful" });
            else
                return Result.Failure(new[] { "Error" });
        }
        public async Task<Result> BlockedUserUpdateAsync(int userId, BlockUserReq request)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.Id == userId))
                return Result.Failure(new[] { "The user must be delete from system. Pls contact to the manager." });
            //
            var userWillBlock = await _dbContext.Users.Where(x => x.Username == request.Username).FirstOrDefaultAsync();
            if (userWillBlock == null)
                return Result.Failure(new[] { "The user to be blocked does not exist in the system" });
            //
            var blockedUser = await _dbContext.Blockings.Where(x => !x.IsDeleted && x.Status && x.ObstructionistUserId == userId && x.BlockedUserId == userWillBlock.Id).FirstOrDefaultAsync();
            if (blockedUser == null)
                return Result.Failure(new[] { "There is no user to blocked" });
            //
            blockedUser.DateOfUpdate = DateTime.Now;
            blockedUser.Status = true;
            _dbContext.Blockings.Update(blockedUser);
            var result = await _dbContext.SaveChangesAsync(new CancellationToken());
            if (result > 0)
                return Result.Success(new[] { "Successful" });
            else
                return Result.Failure(new[] { "Error" });
        }

        public async Task<Result> BlockRemoveAsync(int userId, BlockUserReq request)
        {
            if (!await _dbContext.Users.AnyAsync(x => x.Id == userId))
                return Result.Failure(new[] { "The user must be delete from system. Pls contact to the manager." });
            //
            var userWillBlock = await _dbContext.Users.Where(x => x.Username == request.Username).FirstOrDefaultAsync();
            if (userWillBlock == null)
                return Result.Failure(new[] { "The user to be blocked does not exist in the system" });
            //
            var blockedUser = await _dbContext.Blockings.Where(x => !x.IsDeleted && x.ObstructionistUserId == userId && x.BlockedUserId == userWillBlock.Id).FirstOrDefaultAsync();
            if (blockedUser == null)
                return Result.Failure(new[] { "There is no user to blocked" });
            //
            blockedUser.IsDeleted = true;
            blockedUser.DateOfDelete = DateTime.Now;
            _dbContext.Blockings.Update(blockedUser);
            var result = await _dbContext.SaveChangesAsync(new CancellationToken());
            if (result > 0)
                return Result.Success(new[] { "Successful" });
            else
                return Result.Failure(new[] { "Error" });
        }
    }
}
