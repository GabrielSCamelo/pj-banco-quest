using System.ComponentModel.DataAnnotations;

namespace pj_banco_quest.Models
{
    public class Simulado
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Today;
        public DateTime DataExpiracao { get; set; } = DateTime.Today.AddDays(30);
        public int CriadorId { get; set; } // Id do aluno ou professor que criou o simulado
        public virtual required Usuario Criador { get; set; }
        public bool CriadoPorAluno { get; set; }
        [Range(5, 25, ErrorMessage = "A quantidade de questões deve estar entre 5 e 25.")]
        public int QuantidadeQuestoes { get; set; }
        public List<int> disciplinas { get; set; } = new List<int>();
        public List<int> Questoes { get; set; } = new List<int>();
        public List<int> Turmas { get; set; } = new List<int>();
        public bool Ativo
        {
            get
            {
                return (DateTime.Today - DataCriacao).TotalDays <= 30;
            }
        }
    }
}