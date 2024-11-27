using auction_webapi.BL;
using auction_webapi.DTO;
using auction_webapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auction_webapi.Controllers
{
  
    [ApiController]
    [Route("api/[controller]")]
   
    public class PresentController : ControllerBase
    {
        private IPresentService _presentService;
        private IMapper _imapper;
        public PresentController(IPresentService presentService, IMapper imapper)
        {
            this._presentService = presentService ?? throw new ArgumentNullException(nameof(presentService));
            this._imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }

        [HttpGet("GetPresents/{orderBy}")]
        public async Task<ActionResult<List<Present>>> GetPresents(string orderBy)
        {
            return await _presentService.GetAsync(orderBy);
        }

        [HttpGet("present/{id}")]
        public async Task<ActionResult<Present>> GetPresentById(int id)
        {
            return await _presentService.GetByIdAsync(id);
        }

        [HttpGet("present/filter")]
        public async Task<ActionResult<List<Present>>> GetPresentByFilter(int? amount, string? donarname, string? name)
        {
            return await _presentService.GetByFilterAsync( amount, donarname,  name);
        }
        [HttpGet("GetPresentByCategory/{category}")]
        public async Task<ActionResult<List<Present>>> GetPresentByCategory(int category)
        {
            return await _presentService.GetPresentByCategory(category);
        }


        [HttpDelete("deletePresent/{id}")]
        public async Task<ActionResult<string>> DeletePresent(int id)
        {
            return await _presentService.DeleteByIdAsync(id);
        }

        [HttpPost("addPresent")]
        public async Task<ActionResult<string>> PostPresent([FromBody] PresentDTO present)
        {
            try
            {
                Present p = _imapper.Map<PresentDTO, Present>(present);
                return await _presentService.PostAsync(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("updatePresent")]
        public async Task<ActionResult<string>> UpdatePresent([FromBody] Present present)
        {
            try
            {
                return await _presentService.PutAsync(present);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
