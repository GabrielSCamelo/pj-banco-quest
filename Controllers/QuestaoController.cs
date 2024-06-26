using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pj_banco_quest.Data;
using pj_banco_quest.Models;

namespace pj_banco_quest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestaoController : ControllerBase
    {
        private readonly ContextDb _context;

        public QuestaoController(ContextDb context)
        {
            _context = context;
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetListarAsync([FromQuery] int? disciplinaId = null, string? titulo = null)
        {
            IQueryable<Questao> query = _context.Questoes;

            if (disciplinaId.HasValue)
            {
                query = query.Where(q => q.DisciplinaId == disciplinaId);
            }

            if (!string.IsNullOrEmpty(titulo))
            {
                query = query.Where(q => q.Titulo.Contains(titulo));
            }

            var questoes = await query.ToListAsync();

            return Ok(questoes);
        }

        // Método para salvar questões
        [HttpPost("SalvarTodas")]
        public async Task<IActionResult> PostSalvarTodasAsync([FromBody] List<Questao> questoes)
        {
            if (questoes == null || !questoes.Any())
            {
                return BadRequest("Nenhuma questão para salvar.");
            }

            await _context.Questoes.AddRangeAsync(questoes);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
