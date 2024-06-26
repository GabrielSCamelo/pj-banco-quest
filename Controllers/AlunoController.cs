using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pj_banco_quest.Data;
using pj_banco_quest.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly ContextDb _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AlunoController(ContextDb context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet("Details")]
        public async Task<IActionResult> GetDetailsAsync([FromQuery] int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return Ok(aluno);
        }

        [HttpGet("Listar")]
        public async Task<IActionResult> GetListarAsync()
        {
            var alunos = await _context.Alunos.ToListAsync();
            return Ok(alunos);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> PostCreateAsync([FromBody] Aluno Model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _context.Alunos.AnyAsync(a => a.Matricula == Model.Matricula))
            {
                return BadRequest("Matrícula já cadastrada");
            }

            if (await _context.Alunos.AnyAsync(a => a.Email == Model.Email))
            {
                return BadRequest("Email já cadastrado");
            }

            var user = new IdentityUser
            {
                UserName = Model.Matricula,
                Email = Model.Email
            };

            var result = await _userManager.CreateAsync(user, Model.Senha);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!await _userManager.IsInRoleAsync(user, Roles_DB.Roles.Aluno))
            {
                await _userManager.AddToRoleAsync(user, Roles_DB.Roles.Aluno);
            }

            Model.UserId = user.Id;

            _context.Alunos.Add(Model);
            await _context.SaveChangesAsync();

            return Ok(Model);
        }
    }
}
