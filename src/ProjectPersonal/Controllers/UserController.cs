using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectPersonal.Application.Feature.LoginValid.Commands;
using ProjectPersonal.Application.Feature.Users.Commands.Create;
using ProjectPersonal.Application.Feature.Users.Commands.Delete;
using ProjectPersonal.Application.Feature.Users.Commands.Update;
using ProjectPersonal.Application.Feature.Users.Querys.Read;
using ProjectPersonal.Domain.Enum;

namespace ProjectPersonal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.Code != (int)Eerrors.Success)
            {
                return BadRequest(result);
            }    
            return Ok(result);
        }
        [HttpPost("checkemail")]
        [AllowAnonymous]
        public async Task<IActionResult> CheckEmailEsxits(EmailExitsValidCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Code == (int)Eerrors.Notfound)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllUserQuery());
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand { Id = id};
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserCommand updateUser)
        {
            var result = await _mediator.Send(updateUser);
            return Ok(result);
        }
    }
}
