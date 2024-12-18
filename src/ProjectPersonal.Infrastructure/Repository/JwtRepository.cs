using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Token;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ProjectPersonal.Domain.Entities;
namespace ProjectPersonal.Infrastructure.Repository
{
    public class JwtRepository : IJwtRepository
    {
        private readonly JwtSettings _settings;
        private readonly IUnitofwork<RefreshToken> _unitofwork;
        public JwtRepository(IOptions<JwtSettings> setting, IUnitofwork<RefreshToken> unitOfWork)
        {
            _settings = setting.Value;
            if (string.IsNullOrEmpty(_settings.Key))
            {
                throw new Exception("Jwt secret not configured");
            }
            _unitofwork = unitOfWork;
        }
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Key!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Email", user.Email),
                    new Claim("id", user.Id.ToString()),
                    new Claim("IsAuthenticated", "true"),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_settings.DurationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                RefreshTokenHash = await getUniqueToken(),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
            };
            return refreshToken;
            async Task<string> getUniqueToken()
            {
                var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
                var tokens = await _unitofwork.GetRepository<RefreshToken, Guid>()
                    .FindByCondition(x => x.RefreshTokenHash == token).FirstOrDefaultAsync();
                if (tokens != null)
                {
                    return await getUniqueToken();
                }
                return token;
            }
        }

        public Guid? ValidateJwtToken(string? token)
        {
            if (token == null)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Key!);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _settings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _settings.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                return userId;
            }
            catch
            (Exception ex)
            {
                return null;
            }
        }
    }
}
