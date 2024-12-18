using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Token;

namespace ProjectPersonal.Middleware
{
    public class AddTokenToHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _settings;

        public AddTokenToHeaderMiddleware(RequestDelegate next, IOptions<JwtSettings> appSettings)
        {
            _next = next;
            _settings = appSettings.Value;
        }
        public async Task Invoke(HttpContext context, IUnitofwork<User> unitOfWork, IJwtRepository jwtRepository)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtRepository.ValidateJwtToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                context.Items["user"] = await unitOfWork.GetRepository<User, Guid>()
                    .FindByCondition(x => x.Id == userId)
                    .FirstOrDefaultAsync();
            }
            await _next(context);
        }
    }
}
