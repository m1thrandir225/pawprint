
using Microsoft.AspNetCore.Mvc;
using Web.Services.Interfaces;

namespace Web.Controllers
{
    [Route("/api/upload-controller")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService _uploadService;

        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }
        [HttpPost("upload")]
        public async Task<ActionResult<string>> UploadFile([FromForm] IFormFile file)
        {
            var uploadedFileName = await _uploadService.UploadFile(file);

            return Ok(uploadedFileName);
        }
        [HttpPost("delete")]
        public ActionResult<bool> RemoveFile([FromForm] string fileName)
        {
            var removed =  _uploadService.RemoveFile(fileName);

            if(!removed)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPost("replace")]
        public async Task<ActionResult<string>> ReplaceFile([FromForm] IFormFile file, [FromForm] string currentFileName)
        {
            var uploadedFileName = await _uploadService.ReplaceFile(file, file.FileName);

            return Ok(uploadedFileName);
        }
    }
}