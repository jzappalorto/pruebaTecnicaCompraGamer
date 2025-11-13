using MiBackend.Data;
using MiBackend.Domain.Entities;
using MiBackend.Domain.Interfaces;
using MiBackend.API.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiBackend.API.DTO.Chicos;

namespace MiBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChicosController : ControllerBase
    {
        private readonly IChicoService _chicoService;

        public ChicosController(IChicoService chicoService)
        {
            _chicoService = chicoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChicoReadDto>>> GetAll()
        {
            var list = await _chicoService.GetAllAsync();
            var dtos = list.Select(ToReadDto);
            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ChicoReadDto?>> GetById(int id)
        {
            var chico = await _chicoService.GetByIdAsync(id);
            if (chico == null) return NotFound();
            return Ok(ToReadDto(chico));
        }

        [HttpGet("micro/{microId:int}")]
        public async Task<ActionResult<IEnumerable<ChicoReadDto>>> GetByMicro(int microId)
        {
            var list = await _chicoService.GetAllByMicroAsync(microId);
            var dtos = list.Select(ToReadDto);
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<ChicoReadDto>> Create([FromBody] ChicoCreateDto dto)
        {
            var chico = new Chico
            {
                DNI = dto.DNI,
                Nombre = dto.Nombre,
                MicroId = dto.MicroId
            };

            var created = await _chicoService.CreateAsync(chico);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ToReadDto(created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ChicoReadDto?>> Update(int id, [FromBody] ChicoUpdateDto dto)
        {
            var chico = new Chico
            {
                DNI = dto.DNI,
                Nombre = dto.Nombre,
                MicroId = dto.MicroId
            };

            var updated = await _chicoService.UpdateAsync(id, chico);
            if (updated == null) return NotFound();
            return Ok(ToReadDto(updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var removed = await _chicoService.DeleteAsync(id);
            if (!removed) return NotFound();
            return NoContent();
        }

        [HttpPost("{id:int}/assign/{microId:int}")]
        public async Task<IActionResult> AssignToMicro(int id, int microId)
        {
            var ok = await _chicoService.AssignToMicroAsync(id, microId);
            if (!ok) return BadRequest(new { message = "Asignación fallida. Comprueba ids." });
            return NoContent();
        }

        private static ChicoReadDto ToReadDto(Chico c)
        {
            return new ChicoReadDto
            {
                Id = c.Id,
                DNI = c.DNI,
                Nombre = c.Nombre,
                MicroId = c.MicroId
            };
        }
    }
}
