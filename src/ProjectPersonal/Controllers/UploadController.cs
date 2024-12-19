using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectPersonal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        public IActionResult Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File không hợp lệ");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var relativePath = $"/uploads/{file.FileName}";
            return Ok(new { path = relativePath });
        }
    }
}
