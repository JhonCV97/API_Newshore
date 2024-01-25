using Application.Cqrs.Auth.Commands;
using Application.DTOs.User;
using Application.ViewModel.Auth;

namespace Application.Interfaces.Auths
{
    public interface IAuthService
    {
        Task<UserDto> GetUserByLogin(string login);
        Task<AuthViewModel> GetAuth(PostLoginCommand auth);
        Task<UserDto> GetUserById(Guid? Id);
    }
}
