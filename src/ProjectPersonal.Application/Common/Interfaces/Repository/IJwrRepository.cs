using ProjectPersonal.Domain.Entities;

namespace ProjectPersonal.Application.Common.Interfaces.Repository
{
    public interface IJwtRepository
    {
        public string GenerateJwtToken(User user);
        public Guid? ValidateJwtToken(string? token);
        Task<RefreshToken> GenerateRefreshToken(User user);
    }
}
