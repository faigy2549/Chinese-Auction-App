using auction_webapi.BL;
using auction_webapi.DAL;
using auction_webapi.DTO;
using auction_webapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace auction_webapi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DonarController : ControllerBase
    {

        private IDonarService _donarService;
        private IMapper _imapper;
        public DonarController(IDonarService donarService, IMapper imapper)
        {
            this._donarService = donarService ?? throw new ArgumentNullException(nameof(donarService));
            this._imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }

        [HttpGet("GetDonars")]
        public async Task<ActionResult<List<DonarDTO>>> GetDonars()
        {
            List<Donar> donars = await _donarService.GetAsync();
            List<DonarDTO> donarDTOs = _imapper.Map<List<Donar>, List<DonarDTO>>(donars);

            foreach (Donar donar in donars)
            {
                IEnumerable<PresentDTO> presentDTOs = _imapper.Map<ICollection<Present>, IEnumerable<PresentDTO>>(donar.Presents);
                donarDTOs.First(d => d.Id == donar.Id).Presents = presentDTOs.ToList();
            }

            return donarDTOs;
        }

        [HttpGet("donar/{id}")]
        public async Task<ActionResult<DonarDTO>> GetDonarById(int id)
        {
            Donar donars = await _donarService.GetByIdAsync(id);
            DonarDTO donarDTOs = _imapper.Map<Donar,DonarDTO>(donars);
            IEnumerable<PresentDTO> presentDTOs = _imapper.Map<ICollection<Present>, IEnumerable<PresentDTO>>(donars.Presents);
            donarDTOs.Presents = presentDTOs.ToList();
            return donarDTOs;
        }

        [HttpGet("donar/filter")]
        public async Task<List<DonarDTO>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email)
        {
            List<Donar> donars = await _donarService.GetByFilterAsync(name,lname,presentid,email);
            List<DonarDTO> donarDTOs = _imapper.Map<List<Donar>, List<DonarDTO>>(donars);

            foreach (Donar donar in donars)
            {
                IEnumerable<PresentDTO> presentDTOs = _imapper.Map<ICollection<Present>, IEnumerable<PresentDTO>>(donar.Presents);
                donarDTOs.First(d => d.Id == donar.Id).Presents = presentDTOs.ToList();
            }

            return donarDTOs;
        }



        [HttpDelete("deleteDonar/{id}")]
        public async Task<ActionResult<string>> DeleteDonar(int id)
        {
            return await _donarService.DeleteAsync(id);
        }
        [HttpPost("addDonar")]
        public async Task<ActionResult<Donar>> PostDonar([FromBody] DonarDTO donar)
        {
            try
            {
                Donar d = _imapper.Map<DonarDTO, Donar>(donar);
                return await _donarService.PostAsync(d);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("updateDonar")]
        public async Task<ActionResult<string>> UpdateDonar([FromBody] DonarDTO donar)
        {   
            try
            {
                Donar d = _imapper.Map<DonarDTO, Donar>(donar);
                return await _donarService.PutAsync(d);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
