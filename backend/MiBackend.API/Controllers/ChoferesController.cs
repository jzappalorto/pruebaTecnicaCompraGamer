using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiBackend.Domain.Entities;
using MiBackend.Domain.Interfaces;
using MiBackend.API.DTO;

namespace MiBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChoferesController : ControllerBase
    {
        private readonly IChoferService _choferService;

        public ChoferesController(IChoferService choferService)
        {
            _choferService = choferService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChoferReadDto>>> GetAll()
        {
            var list = await _choferService.GetAllAsync();
            var dtos = list.Select(ToReadDto);
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ChoferReadDto?>> GetById(int id)
        {
            var chofer = await _choferService.GetByIdAsync(id);
            if (chofer == null) return NotFound();
            return Ok(ToReadDto(chofer));
        }

        //[HttpGet("micro/{microId:int}")]
        //public async Task<ActionResult<IEnumerable<ChoferReadDto>>> GetByMicro(int microId)
        //{
        //    var list = await _choferService.GetAllByMicroAsync(microId);
        //    var dtos = list.Select(ToReadDto);
        //    return Ok(dtos);
        //}

        [HttpPost]
        public async Task<ActionResult<ChoferReadDto>> Create([FromBody] ChoferCreateDto dto)
        {
            var chofer = new Chofer
            {
                DNI = dto.DNI,
                Nombre = dto.Nombre,
                MicroId = dto.MicroId
            };

            var created = await _choferService.CreateAsync(chofer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ToReadDto(created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ChoferReadDto?>> Update(int id, [FromBody] ChoferUpdateDto dto)
        {
            var chofer = new Chofer
            {
                DNI = dto.DNI,
                Nombre = dto.Nombre,
                MicroId = dto.MicroId
            };

            var updated = await _choferService.UpdateAsync(id, chofer);
            if (updated == null) return NotFound();
            return Ok(ToReadDto(updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removed = await _choferService.DeleteAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }

        [HttpPost("{id:int}/assign/{microId:int}")]
        public async Task<IActionResult> AssignToMicro(int id, int microId)
        {
            var ok = await _choferService.AssignToMicroAsync(id, microId);
            if (!ok) return BadRequest(new { message = "Asignación fallida. Comprueba ids." });
            return NoContent();
        }

        private static ChoferReadDto ToReadDto(Chofer c)
        {
            return new ChoferReadDto
            {
                Id = c.Id,
                DNI = c.DNI,
                Nombre = c.Nombre,
                MicroId = c.MicroId
            };
        }
    }
}
