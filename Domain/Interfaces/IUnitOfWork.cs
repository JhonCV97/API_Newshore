using Domain.Models.Role;
using Domain.Models.User;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Role> RoleRepository { get; }
        IRepository<User> UserRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
