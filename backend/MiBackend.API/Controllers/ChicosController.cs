using MiBackend.Data;
using MiBackend.Domain.Entities;
using MiBackend.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<Chico>>> GetAll()
        {
            var list = await _chicoService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Chico?>> GetById(int id)
        {
            var chico = await _chicoService.GetByIdAsync(id);
            if (chico == null) return NotFound();
            return Ok(chico);
        }

        [HttpGet("micro/{microId:int}")]
        public async Task<ActionResult<IEnumerable<Chico>>> GetByMicro(int microId)
        {
            var list = await _chicoService.GetAllByMicroAsync(microId);
            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<Chico>> Create([FromBody] Chico chico)
        {
            var created = await _chicoService.CreateAsync(chico);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Chico?>> Update(int id, [FromBody] Chico chico)
        {
            var updated = await _chicoService.UpdateAsync(id, chico);
            if (updated == null) return NotFound();
            return Ok(updated);
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
    }
}
