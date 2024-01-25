using Application.DTOs.User;

namespace Application.ViewModel.Auth
{
    public class AuthViewModel
    {
        public string? token { get; set; }
        public UserDto? user { get; set; }

    }
}
