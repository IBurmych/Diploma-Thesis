using Diploma_Thesis.Models;
using Diploma_Thesis.Services;
using Microsoft.AspNetCore.Mvc;

namespace Diploma_Thesis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ExpertisesController : ControllerBase
    {
        private readonly IExpertisesService _expertisesService;
        public ExpertisesController(IExpertisesService expertisesService)
        {
            _expertisesService = expertisesService;
        }
        [HttpGet]
        public IActionResult GetByClientId(Guid id)
        {
            var expertises = _expertisesService.GetByClientId(id);
            return Ok(expertises);
        }
        [HttpPost]
        public async Task<IActionResult> Add(ExpertiseModel model)
        {
            int count = await _expertisesService.AddAsync(model);
            if (count < 1)
                return BadRequest();
            return Ok();
        }
    }
}
