using MiBackend.Domain.Entities;
using MiBackend.Domain.Interfaces;
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
        public async Task<ActionResult<IEnumerable<Micro>>> GetAll()
        {
            var list = await _microService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Micro?>> GetById(int id)
        {
            var micro = await _microService.GetByIdAsync(id);
            if (micro == null) return NotFound();
            return Ok(micro);
        }

        [HttpPost]
        public async Task<ActionResult<Micro>> Create([FromBody] Micro micro)
        {
            var created = await _microService.CreateAsync(micro);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Micro?>> Update(int id, [FromBody] Micro micro)
        {
            var updated = await _microService.UpdateAsync(id, micro);
            if (updated == null) return NotFound();
            return Ok(updated);
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

        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<Micro?>> GetWithRelations(int id)
        //{
        //    var micro = await _microService.GetWithRelationsAsync(id);
        //    if (micro == null) return NotFound();
        //    return Ok(micro);
        //}
    }
}
