using Microsoft.AspNetCore.Mvc;

namespace Diploma_Thesis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadFile([FromQuery] Guid clientId)
        {
            var file = Request.Form.Files[0]; 

            if (file != null && file.Length > 0)
            {
                var fileName = file.FileName;
                var fileSize = file.Length;
            }

            return Ok(true);
        }
    }
}
