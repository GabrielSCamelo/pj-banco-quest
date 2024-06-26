using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pj_banco_quest.Data;
using pj_banco_quest.Models;
using System.Security.Claims;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimuladoController : Controller
    {
        private readonly ContextDb _context;

        public SimuladoController(ContextDb context)
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

            var simulado = await _context.Simulados.FindAsync(id);

            if (simulado == null)
            {
                return NotFound();
            }

            return Ok(simulado);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetListarAsync()
        {
            var simulados = await _context.Simulados.Where(p => p.Ativo == true).ToListAsync();
            return Ok(simulados);
        }

        [HttpPost("Create")]
        public async Task<ActionResult<Simulado>> PostCreateAsync([FromBody] Simulado simulado)
        {
            if (simulado.QuantidadeQuestoes < 5 || simulado.QuantidadeQuestoes > 25)
            {
                return BadRequest("Quantidade de questões deve ser entre 5 e 25.");
            }

            var questoes = await _context.Questoes
                .Where(q => simulado.disciplinas.Contains(q.Disciplina.Id))
                .OrderBy(_ => Guid.NewGuid())
                .Take(simulado.QuantidadeQuestoes)
                .ToListAsync();

            if (questoes.Count != simulado.QuantidadeQuestoes)
            {
                return BadRequest("Não há questões suficientes no banco de dados.");
            }

            simulado.Questoes = questoes.Select(p => p.Id).ToList();

            var userRole = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrEmpty(userRole))
            {
                return Unauthorized("Usuário não tem role definida.");
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized("ID de usuário não encontrado no token.");
            }

            simulado.CriadoPorAluno = userRole == "Aluno";

            var criador = simulado.CriadoPorAluno ?
                await _context.Alunos.FirstOrDefaultAsync(p => p.UserId == userIdClaim) as object :
                await _context.Professores.FirstOrDefaultAsync(p => p.UserId == userIdClaim) as object;

            if (criador == null)
            {
                return Unauthorized("Usuário não encontrado no banco de dados.");
            }

            simulado.CriadorId = criador?.GetType().GetProperty("Id")?.GetValue(criador) as int? ?? 0;

            if (simulado.CriadoPorAluno)
            {
                var user = await _context.Alunos.FindAsync(simulado.CriadorId);
                if (user == null)
                {
                    return NotFound("Aluno não encontrado.");
                }

                var alunoSimulado = new Simulado_Aluno
                {
                    AlunoId = simulado.CriadorId,
                    SimuladoId = simulado.Id,
                    Simulado = simulado,
                    Aluno = user
                };
                _context.Simulados_Alunos.Add(alunoSimulado);
            }
            else
            {
                var turmas = await _context.Turmas
                    .Where(t => t.ProfessorId == simulado.CriadorId)
                    .ToListAsync();

                var alunosTurma = await _context.Alunos_Turmas
                    .Where(at => turmas.Any(t => t.Id == at.TurmaId))
                    .ToListAsync();

                foreach (var aluno in alunosTurma)
                {
                    var alunoSimulado = new Simulado_Aluno
                    {
                        SimuladoId = simulado.Id,
                        AlunoId = aluno.AlunoId,
                        Simulado = simulado,
                        Aluno = aluno.aluno
                    };
                    _context.Simulados_Alunos.Add(alunoSimulado);
                }
            }

            await _context.SaveChangesAsync();

            return Ok(simulado);
        }
    }
}