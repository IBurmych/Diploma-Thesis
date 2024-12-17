using Diploma_Thesis.Models;
using Diploma_Thesis.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Diploma_Thesis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DiapasonsController : ControllerBase
    {
        private readonly IDiapasonService _diapasonService;
        public DiapasonsController(IDiapasonService diapasonService)
        {
            _diapasonService = diapasonService;
        }

        [HttpPost]
        public IActionResult GetCrossing(DiapasonModel[] diapasons)
        {
            if (diapasons.IsNullOrEmpty())
            {
                return BadRequest();
            }
            var crossing = _diapasonService.GetСrossing(diapasons[0], diapasons[1]);
            if (crossing == null)
            {
                return BadRequest();
            }
            return Ok(crossing);
        }
        [HttpPost]
        public async Task<IActionResult> SaveCrosing(List<FullDiapasonModel> diapasons, [FromQuery] string name)
        {
            if (diapasons.IsNullOrEmpty())
            {
                return BadRequest();
            }
            diapasons.ForEach(x => x.CalcName = name);
            var crossing = await _diapasonService.SaveDiapasons(diapasons);

            if (crossing.IsNullOrEmpty())
            {
                return BadRequest();
            }
            return Ok(crossing);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _diapasonService.GetAllAsync());
        }
    }
}
