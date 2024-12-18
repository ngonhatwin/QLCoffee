using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectPersonal.Application.Feature.Login.Google.Commands;
using ProjectPersonal.Application.Feature.Login.Oauth.Commands.Token;
using ProjectPersonal.Application.Feature.LoginValid.Commands;
using ProjectPersonal.Domain.Enum;

namespace ProjectPersonal.Controllers
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(CreateTokenCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("google-login")]
        [AllowAnonymous]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
