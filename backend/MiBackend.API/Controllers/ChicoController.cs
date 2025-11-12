using MiBackend.Data;
using MiBackend.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MiBackend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChicoController : ControllerBase
    {
        private readonly MiBackendContext _context;

        public ChicoController(MiBackendContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chico>>> GetChicos()
        {
            var chicos = await _context.Chicos.ToListAsync();
            return Ok(chicos);
        }
    }
}
