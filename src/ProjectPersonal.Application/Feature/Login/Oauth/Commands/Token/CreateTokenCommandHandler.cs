using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using ProjectPersonal.Domain.Entities;
using ProjectPersonal.Domain.Enum;
namespace ProjectPersonal.Application.Feature.Login.Oauth.Commands.Token
{
    public class CreateTokenCommandHandler : IRequestHandler<CreateTokenCommand, Result<AuthenResponse>>
    {
        private readonly IJwtRepository _jwtRepository;
        private readonly IUnitofwork<User> _unitOfWork;
        public CreateTokenCommandHandler(IJwtRepository jwtRepository, IUnitofwork<User> unitOfWork)
        {
            _jwtRepository = jwtRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<AuthenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _unitOfWork.GetRepository<User, Guid>()
                    .FindByCondition(x => x.Email == request.Email)
                    .FirstOrDefaultAsync();
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    return new Result<AuthenResponse>
                    {
                        Code = (int)Eerrors.EmailOrPass_Incorrect,
                        Message = "Email or password incorect"
                    };
                }
                if (user == null)
                {
                    return new Result<AuthenResponse>
                    {
                        Code = (int)Eerrors.Notfound,
                        Message = "Not found",
                    };
                }
                var oldToken = await _unitOfWork.GetRepository<RefreshToken, Guid>()
                    .FindByCondition(x => x.UserId == user.Id).ToListAsync();
                var role = (byte)user.Role;
                var userName = user.Username;
                if (oldToken.Count > 0)
                {
                    foreach (var i in oldToken)
                    {
                        await _unitOfWork.GetRepository<RefreshToken, Guid>().DeleteAsync(i);
                    }
                    await _unitOfWork.GetRepository<RefreshToken, Guid>().SaveChangesAsync();
                }
                var token = _jwtRepository.GenerateJwtToken(user);
                var refreshToken = _jwtRepository.GenerateRefreshToken(user);

                await _unitOfWork.GetRepository<RefreshToken, Guid>().CreateAsync(refreshToken.Result);
                await _unitOfWork.GetRepository<RefreshToken, Guid>().SaveChangesAsync();
                return new Result<AuthenResponse>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = new AuthenResponse
                    {
                        Username = userName,
                        Token = token,
                        RefreshTokenHash = refreshToken.Result.RefreshTokenHash,
                        ExpiresAt = refreshToken.Result.ExpiresAt,
                        CreatedAt = refreshToken.Result.CreatedAt,
                        Role = role,
                    }
                };
            }
            catch (Exception ex)
            {
                return new Result<AuthenResponse>
                {
                    Code = (int)Eerrors.Exceptions,
                    Message = ex.Message,
                };
            }
        }
    }
}
