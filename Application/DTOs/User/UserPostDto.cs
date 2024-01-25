namespace Application.DTOs.User
{
    public class UserPostDto
    {
        public string? Document { get; set; }
        public string? Names { get; set; }
        public string? Phone1 { get; set; }
        public string? Email { get; set; }
        public bool Status { get; set; } = true;
        public bool Active { get; set; } = true;
        public string? Password { get; set; }
        public string? Login { get; set; }
        public Guid? RoleId { get; set; }

    }
}
