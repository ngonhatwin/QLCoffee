using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Request;
using ProjectPersonal.Application.Dtos.Response;

namespace ProjectPersonal.Application.Common.Interfaces.Repository
{
    public interface ITokenRepository
    {
        Task<Result<AuthenResponse>> AuthenticateAsync(AuthenRequest request, CancellationToken cancellationToken);
        Task<AuthenResponse> RefreshToken(string token);
        void RevokeToken(string token);
        Task<User> GetUserByRefreshToken(string? token);
        Task<User> GetUserByRefreshToken2(string? token);
    }
}
