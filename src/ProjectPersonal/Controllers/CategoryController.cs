using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectPersonal.Application.Feature.Category.Commands.Create;
using ProjectPersonal.Application.Feature.Category.Querys.Read;
using System.Net.WebSockets;

namespace ProjectPersonal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll ()
        {
            var command = new GetAllCategoryQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
