using System.Linq;
using MiBackend.Domain.Entities;
using MiBackend.Domain.Interfaces;
using MiBackend.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MiBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MicrosController: ControllerBase
    {
        private readonly IMicroService _microService;

        public MicrosController(IMicroService microService)
        {
            _microService = microService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MicroReadDto>>> GetAll()
        {
            var list = await _microService.GetAllAsync();
            var dtos = list.Select(ToReadDto);
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MicroReadDto?>> GetById(int id)
        {
            var micro = await _microService.GetByIdAsync(id);
            if (micro == null) return NotFound();
            return Ok(ToReadDto(micro));
        }

        [HttpPost]
        public async Task<ActionResult<MicroReadDto>> Create([FromBody] MicroCreateDto dto)
        {
            var micro = new Micro
            {
                Patente = dto.Patente,
                MarcaModelo = dto.MarcaModelo
            };

            var created = await _microService.CreateAsync(micro);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ToReadDto(created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<MicroReadDto?>> Update(int id, [FromBody] MicroUpdateDto dto)
        {
            var micro = new Micro
            {
                Patente = dto.Patente,
                MarcaModelo = dto.MarcaModelo
            };

            var updated = await _microService.UpdateAsync(id, micro);
            if (updated == null) return NotFound();
            return Ok(ToReadDto(updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var removed = await _microService.DeleteAsync(id);
                if (!removed) return NotFound();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        private static MicroReadDto ToReadDto(Micro m)
        {
            return new MicroReadDto
            {
                Id = m.Id,
                Patente = m.Patente,
                MarcaModelo = m.MarcaModelo,
                Chofer = m.Chofer == null ? null : new ChoferReadDto
                {
                    Id = m.Chofer.Id,
                    DNI = m.Chofer.DNI,
                    Nombre = m.Chofer.Nombre,
                    MicroId = m.Chofer.MicroId
                },
                Chicos = m.Chicos?.Select(c => new ChicoReadDto
                {
                    Id = c.Id,
                    DNI = c.DNI,
                    Nombre = c.Nombre,
                    MicroId = c.MicroId
                }).ToList()
            };
        }
    }
}
