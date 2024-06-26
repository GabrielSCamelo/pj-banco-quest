using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pj_banco_quest.Data;
using pj_banco_quest.Models;

namespace pj_banco_quest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoTurmaController : ControllerBase
    {
        private readonly ContextDb _context;

        public AlunoTurmaController(ContextDb context)
        {
            _context = context;
        }

        // Endpoint para associar um aluno a uma turma
        [HttpPost("AssociarAlunoTurma")]
        public async Task<IActionResult> AssociarAlunoTurma([FromBody] Aluno_Turma alunoTurma)
        {
            try
            {
                // Verifica se a associação já existe
                if (await _context.Alunos_Turmas.AnyAsync(at => at.AlunoId == alunoTurma.AlunoId && at.TurmaId == alunoTurma.TurmaId))
                {
                    return BadRequest("Essa associação aluno-turma já existe.");
                }

                // Verifica se o aluno e a turma existem no banco
                var alunoExists = await _context.Alunos.AnyAsync(a => a.Id == alunoTurma.AlunoId);
                var turmaExists = await _context.Turmas.AnyAsync(t => t.Id == alunoTurma.TurmaId);

                if (!alunoExists || !turmaExists)
                {
                    return NotFound("Aluno ou Turma não encontrados.");
                }

                _context.Alunos_Turmas.Add(alunoTurma);
                await _context.SaveChangesAsync();

                return Ok("Aluno associado à turma com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao associar aluno à turma: {ex.Message}");
            }
        }

        // Endpoint para listar os alunos associados a uma turma específica
        [HttpGet("ListarAlunosTurma")]
        public async Task<IActionResult> ListarAlunosTurma(int turmaId)
        {
            try
            {
                var alunosTurma = await _context.Alunos_Turmas
                    .Where(at => at.TurmaId == turmaId)
                    .Select(at => at.aluno)
                    .ToListAsync();

                return Ok(alunosTurma);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno ao listar alunos da turma: {ex.Message}");
            }
        }
    }
}

