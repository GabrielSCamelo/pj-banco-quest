using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pj_banco_quest.Data;
using pj_banco_quest.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController : ControllerBase
    {
        private readonly ContextDb _context;

        public DisciplinaController(ContextDb context)
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

            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null)
            {
                return NotFound();
            }

            return Ok(disciplina);
        }

        [HttpGet("Listar")]
        public IActionResult GetListar()
        {
            var disciplinas = _context.Disciplinas.ToList();
            return Ok(disciplinas);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostCreateAsync([FromBody] Disciplina model)
        {
            if (ModelState.IsValid)
            {
                var existingDisciplina = await _context.Disciplinas.FirstOrDefaultAsync(d => d.Nome == model.Nome);
                if (existingDisciplina != null)
                {
                    return BadRequest("Disciplina já cadastrada");
                }

                _context.Disciplinas.Add(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }

            return BadRequest(ModelState);
        }
    }
}
