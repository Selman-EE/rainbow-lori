using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Chat> Chats { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Blocking> Blockings { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}