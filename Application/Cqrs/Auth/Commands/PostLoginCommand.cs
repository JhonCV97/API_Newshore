using Application.Common.Exceptions;
using Application.DTOs.Auth;
using Application.Interfaces.Auths;
using Application.Interfaces.EncryptAndDecrypt;
using Application.ViewModel.Auth;
using MediatR;
using Newtonsoft.Json;

namespace Application.Cqrs.Auth.Commands
{
    public class PostLoginCommand : IRequest<AuthViewModel>
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
    }

    public class PostLoginCommandHandler : IRequestHandler<PostLoginCommand, AuthViewModel>
    {
        private IAuthService _authService;
        
        public PostLoginCommandHandler(
            IAuthService authService
        )
        {
            _authService = authService;
        }

        public async Task<AuthViewModel> Handle(PostLoginCommand request, CancellationToken cancellationToken)
        {
            var authData = await _authService.GetAuth(request) ??
                throw new BadRequestException("No se ha podido ingresar el token");
            

            return authData;
        }
    }

}
