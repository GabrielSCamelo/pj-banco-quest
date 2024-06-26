using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pj_banco_quest.Data;
using pj_banco_quest.Models;
using static pj_banco_quest.Models.Roles_DB;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly ContextDb _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfessorController(ContextDb context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public int CalcularTotalPontos(Simulado_Aluno simulado)
        {
            // Carrega as questões associadas com base nos IDs
            var questoes = _context.Questoes
                .Where(q => simulado.Simulado.Questoes.Contains(q.Id))
                .ToList();

            int total = 0;

            for (int i = 0; i < questoes.Count && i < simulado.Respostas.Count; i++)
            {
                if (questoes[i].OpcaoCorretaIndex == simulado.Respostas[i])
                {
                    total++;
                }
            }

            return total;
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetDetailsAsync([FromQuery] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professores.FindAsync(id);

            if (professor == null)
            {
                return NotFound();
            }

            return Ok(professor);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetListarAsync()
        {
            var professores = await _context.Professores.ToListAsync();
            return Ok(professores);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostCreateAsync([FromBody] Professor model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Professores.Any(p => p.IdFuncional == model.IdFuncional))
            {
                return BadRequest("Matrícula já cadastrada");
            }

            if (_context.Professores.Any(p => p.Email == model.Email))
            {
                return BadRequest("Email já cadastrado");
            }

            var user = new IdentityUser
            {
                UserName = model.IdFuncional,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Senha);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!await _userManager.IsInRoleAsync(user, Roles.Professor))
            {
                await _userManager.AddToRoleAsync(user, Roles.Professor);
            }

            model.UserId = user.Id;

            _context.Professores.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }
    }
}
