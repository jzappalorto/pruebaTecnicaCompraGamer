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

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Micro?>> GetWithRelations(int id)
        {
            var micro = await _microService.GetWithRelationsAsync(id);
            if (micro == null) return NotFound();
            return Ok(micro);
        }
    }
}
