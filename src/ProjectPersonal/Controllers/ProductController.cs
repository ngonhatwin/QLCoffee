using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectPersonal.Application.Feature.Products.Commands.Create;
using ProjectPersonal.Application.Feature.Products.Commands.Delete;
using ProjectPersonal.Application.Feature.Products.Commands.Update;
using ProjectPersonal.Application.Feature.Products.Querys.Read;

namespace ProjectPersonal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Tạo một sản phẩm mới trong hệ thống.
        /// </summary>
        /// <param name="command">Thông tin sản phẩm cần tạo.</param>
        /// <returns>Mã định danh của sản phẩm được tạo.</returns>
        /// <response code="201">Sản phẩm được tạo thành công.</response>
        /// <response code="400">Thông tin không hợp lệ.</response>
        /// <response code="500">Lỗi từ hệ thống.</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result); 
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result);
        }

        [HttpGet("count-product")]
        public async Task<IActionResult> CountProducts()
        {
            var result = await _mediator.Send(new CountProductsQuery());
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteProductCommand { Id = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMany(List<Guid> ids)
        {
            var command = new DeleteManyProductsCommand { ProductIds = ids};
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
