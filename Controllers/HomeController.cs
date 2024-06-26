using Microsoft.AspNetCore.Mvc;
using pj_banco_quest.Models;
using System.Diagnostics;

namespace pj_banco_quest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Dashboard() //pagina inicial do sistema
        {
            return View();
        }

        public IActionResult Login() //login do sistema
        {
            return View();
        }

        public IActionResult Simulado() //criar simulado / visualizar simulado / receber relatorio Simulado
        {
            return View();
        }

        public IActionResult Questoes() //criar questões / listar questões / pesquisar questões 
        {
            return View();
        }

        public IActionResult Turma() //criado para criar/listar turmas obs: n foi possivel fazer integração necessario acesso api do outro sistema
        {
            return View();
        }

        public IActionResult Professor() //criado para criar/listar Professor obs: n foi possivel fazer integração necessario acesso api do outro sistema
        {
            return View();
        }

        public IActionResult Aluno() //criado para criar/listar aluno obs: n foi possivel fazer integração necessario acesso api do outro sistema
        {
            return View();
        }
        public IActionResult Disciplina() //criado para criar/listar disicplinas obs: n foi possivel fazer integração necessario acesso api do outro sistema
        {
            return View();
        }
        public IActionResult Aluno_turma() //criado para criar/listar disicplinas obs: n foi possivel fazer integração necessario acesso api do outro sistema
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
