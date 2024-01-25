using Application.Common.Response;
using Application.DTOs.User;
using Application.Interfaces.User;
using MediatR;

namespace Application.Cqrs.User.Commands
{
    public class PostUserCommand : IRequest<ApiResponse<UserDto>>
    {
        public UserPostDto UserPostDto { get; set; }
    }
    public class PostUserCommandHandler : IRequestHandler<PostUserCommand, ApiResponse<UserDto>>
    {
        private readonly IUserService _userService;
        public PostUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ApiResponse<UserDto>> Handle(PostUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.AddUser(request);
        }
    }
}
