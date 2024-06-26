using System.ComponentModel.DataAnnotations.Schema;

namespace pj_banco_quest.Models
{
    public class Turma
    {
        public int Id { get; set; }
        public int DisciplinaId { get; set; }
        public virtual required Disciplina Disciplina { get; set; } = null!;
        public required string Periodo { get; set; }
        public int ProfessorId { get; set; }
        public virtual required Professor Professor { get; set; } = null!;
        public List<Aluno_Turma> aluno_Turmas { get; set; }
    }
}
