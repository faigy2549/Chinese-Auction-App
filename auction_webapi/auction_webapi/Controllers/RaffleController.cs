using auction_webapi.BL;
using auction_webapi.DTO;
using auction_webapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace auction_webapi.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RaffleController : ControllerBase
    {
        private IMapper _imapper;
        private IRaffleService _raffleService;
        public RaffleController(IRaffleService raffleService, IMapper imapper)
        {
           this._raffleService = raffleService ?? throw new ArgumentNullException(nameof(raffleService));
            this._imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }

        [HttpPost("Raffle")]
        public async Task<ActionResult<Winner>> PostAsync([FromBody] PresentDTO present)
        {
            try
            {
                Present p = _imapper.Map<PresentDTO, Present>(present);
                return await _raffleService.Raffle(p);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("Results")]
        public async Task<ActionResult<string>> Results()
        {
            return await _raffleService.Results();
        }


        [HttpGet("Income")]
        public async Task<ActionResult<string>> Income()
        {
            return await _raffleService.Income();
        }


    }
}
