using AutoMapper;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Dtos.Request;
using ProjectPersonal.Application.Dtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPersonal.Infrastructure.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IUnitofwork<User> _unitofwork;
        private readonly IMapper _mapper;
        private readonly IJwtRepository _jwtRepository;
        public TokenRepository(IUnitofwork<User> unitofwork, IMapper mapper, IJwtRepository jwtRepository)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
            _jwtRepository = jwtRepository;
        }

        public async Task<Result<AuthenResponse>> AuthenticateAsync(AuthenRequest request, CancellationToken cancellationToken)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(request.Password, BCrypt.Net.BCrypt.GenerateSalt());
            var user = await _unitofwork.GetRepository<User, Guid>()
                .FindByCondition(x => x.Password == password)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return new Result<AuthenResponse>
                {
                    Code = 4,
                    Message = "Not found"
                };
            }
            var token = _jwtRepository.GenerateJwtToken(user);
            return new Result<AuthenResponse>
            {
                Code = 2,
                Message = "Success",
                Data = new AuthenResponse { Token = token }
            };
        }

        public Task<User> GetUserByRefreshToken(string? token)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByRefreshToken2(string? token)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenResponse> RefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public void RevokeToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
