using ProjectPersonal.Domain.Enum;

namespace ProjectPersonal.Application.Dtos.Response
{
    public class UserResponse
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public Role? Role { get; set; } = Domain.Enum.Role.Customer;
    }
}
