using Google.Apis.Auth;
using Google.Apis.Util;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Response;
using ProjectPersonal.Domain.Entities;
using ProjectPersonal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Application.Feature.Login.Google.Commands
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, Result<AuthenResponse>>
    {
        private readonly IUnitofwork<User> _unitOfWork;
        private readonly IJwtRepository _jwtRepository;

        public GoogleLoginCommandHandler(IUnitofwork<User> unitOfWork, IJwtRepository jwtRepository)
        {
            _unitOfWork = unitOfWork;
            _jwtRepository = jwtRepository;
        }

        public async Task<Result<AuthenResponse>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var jwtToken = string.Empty;
            var refreshToken = new RefreshToken();
            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
            {
                // Thay bằng Client ID
                Audience = new List<string> { "407515638618-to1rr3s1s0uu056vjma88qn00j36c3d3.apps.googleusercontent.com" }
            });

            var userExisted = await _unitOfWork.GetRepository<User, Guid>().FindByCondition(x => x.Email == payload.Email).SingleOrDefaultAsync();
            if (userExisted == null)
            {
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(payload.JwtId, BCrypt.Net.BCrypt.GenerateSalt());
                var user = new User
                {
                    Email = payload.Email,
                    Username = payload.GivenName,
                    Role = Role.Customer,
                    Password = passwordHash,
                    FullName = payload.Name,
                    CreatedDate = DateTime.UtcNow,
                };
                await _unitOfWork.GetRepository<User, Guid>().CreateAsync(user);
                jwtToken = _jwtRepository.GenerateJwtToken(user);
                refreshToken = _jwtRepository.GenerateRefreshToken(user).Result;
                await _unitOfWork.GetRepository<RefreshToken, Guid>().CreateAsync(refreshToken);
                await _unitOfWork.GetRepository<RefreshToken, Guid>().SaveChangesAsync();
                return new Result<AuthenResponse>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = new AuthenResponse
                    {
                        Username = payload.Name,
                        Token = jwtToken,
                        RefreshTokenHash = refreshToken.RefreshTokenHash,
                        ExpiresAt = refreshToken.ExpiresAt,
                        CreatedAt = refreshToken.CreatedAt,
                        Role = (byte)Role.Customer,
                    }
                };
            }
           var refreshTokenExisted = await _unitOfWork.GetRepository<RefreshToken, Guid>()
            .FindByCondition(x => x.UserId == userExisted.Id)
            .FirstOrDefaultAsync();
            if (refreshTokenExisted == null)
            {
                jwtToken = _jwtRepository.GenerateJwtToken(userExisted);
                refreshToken = _jwtRepository.GenerateRefreshToken(userExisted).Result;
                await _unitOfWork.GetRepository<RefreshToken, Guid>().CreateAsync(refreshToken);
                await _unitOfWork.GetRepository<RefreshToken, Guid>().SaveChangesAsync();
                return new Result<AuthenResponse>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = new AuthenResponse
                    {
                        Username = payload.Name,
                        Token = jwtToken,
                        RefreshTokenHash = refreshToken.RefreshTokenHash,
                        ExpiresAt = refreshToken.ExpiresAt,
                        CreatedAt = refreshToken.CreatedAt,
                        Role = (byte)Role.Customer,
                    }
                };
            }
            else
            {
                await _unitOfWork.GetRepository<RefreshToken, Guid>().DeleteAsync(refreshTokenExisted);
                jwtToken = _jwtRepository.GenerateJwtToken(userExisted);
                refreshToken = _jwtRepository.GenerateRefreshToken(userExisted).Result;
                await _unitOfWork.GetRepository<RefreshToken, Guid>().CreateAsync(refreshToken);
                await _unitOfWork.GetRepository<RefreshToken, Guid>().SaveChangesAsync();
                return new Result<AuthenResponse>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = new AuthenResponse
                    {
                        Username = payload.Name,
                        Token = jwtToken,
                        RefreshTokenHash = refreshToken.RefreshTokenHash,
                        ExpiresAt = refreshToken.ExpiresAt,
                        CreatedAt = refreshToken.CreatedAt,
                        Role = (byte)Role.Customer,
                    }
                };
            }
        }
    }
}
