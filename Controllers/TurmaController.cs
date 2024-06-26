using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pj_banco_quest.Data;
using pj_banco_quest.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurmaController : ControllerBase
    {
        private readonly ContextDb _context;

        public TurmaController(ContextDb context)
        {
            _context = context;
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetDetailsAsync([FromQuery] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turmas.FindAsync(id);

            if (turma == null)
            {
                return NotFound();
            }

            return Ok(turma);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetListarAsync()
        {
            var turmas = await _context.Turmas.ToListAsync();
            return Ok(turmas);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostCreateAsync([FromBody] Turma model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Turmas.Any(t => t.DisciplinaId == model.DisciplinaId && t.Periodo == model.Periodo))
            {
                return BadRequest("Turma já cadastrada");
            }

            _context.Turmas.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }
    }
}
