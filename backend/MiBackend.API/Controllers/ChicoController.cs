using MiBackend.Data;
using MiBackend.Domain.Entities;
using MiBackend.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MiBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChicoController : ControllerBase
    {
        private readonly IChicoService _service;

        public ChicoController(IChicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chico>>> GetChicos()
        {
            var chicos = await _service.GetAllAsync();
            return Ok(chicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chico>> GetChico(int id)
        {
            var chico = await _service.GetByIdAsync(id);
            if (chico == null)
                return NotFound();

            return Ok(chico);
        }

        [HttpPost]
        public async Task<ActionResult<Chico>> CreateChico(Chico chico)
        {
            var nuevo = await _service.CreateAsync(chico);
            return CreatedAtAction(nameof(GetChico), new { id = nuevo.Id }, nuevo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Chico>> UpdateChico(int id, Chico chico)
        {
            var actualizado = await _service.UpdateAsync(id, chico);
            if (actualizado == null)
                return NotFound();

            return Ok(actualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChico(int id)
        {
            var eliminado = await _service.DeleteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }
    }
}
