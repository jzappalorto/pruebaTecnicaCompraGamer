using MiBackend.API.DTO;
using MiBackend.Domain.Entities;
using MiBackend.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MiBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ChoferesController: ControllerBase
    {
        private readonly IChoferService _choferService;

        public ChoferesController(IChoferService choferService)
        {
            _choferService = choferService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chofer>>> GetAll()
        {
            var list = await _choferService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Chofer?>> GetById(int id)
        {
            var chofer = await _choferService.GetByIdAsync(id);
            if (chofer == null) return NotFound();
            return Ok(chofer);
        }

        [HttpGet("micro/{microId:int}")]
        public async Task<ActionResult<Chofer?>> GetByMicro(int microId)
        {
            var chofer = await _choferService.GetByMicroAsync(microId);
            if (chofer == null) return NotFound();
            return Ok(chofer);
        }

        [HttpPost]
        public async Task<ActionResult<Chofer>> Create([FromBody] ChoferCreateDto dto)
        {
            var chofer = new Chofer
            {
                DNI = dto.DNI,
                Nombre = dto.Nombre,
                MicroId = dto.MicroId
            };

            var created = await _choferService.CreateAsync(chofer);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Chofer?>> Update(int id, [FromBody] Chofer chofer)
        {
            var updated = await _choferService.UpdateAsync(id, chofer);
            if (updated == null) return NotFound();
            return Ok(updated);
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
            if (!ok) return Conflict(new { message = "El micro ya tiene otro chofer o ids inválidos." });
            return NoContent();
        }
    }
}
