using Diploma_Thesis.Models;
using Diploma_Thesis.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma_Thesis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IExpertisesService _expertisesService;
        public ImageController(IExpertisesService expertisesService)
        {
            _expertisesService = expertisesService;
        }
        [HttpPost]
        public async Task<IActionResult> UploadFile([FromQuery] Guid clientId)
        {
            var file = Request.Form.Files[0];
            ExpertiseModel? expertise = null;

            if (file != null && file.Length > 0)
            {
                expertise = await _expertisesService.AnalyzePhoto(file, clientId);
            }

            return Ok(expertise);
        }
    }
}
