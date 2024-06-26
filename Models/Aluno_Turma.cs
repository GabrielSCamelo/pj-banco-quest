namespace pj_banco_quest.Models
{
    public class Aluno_Turma
    {
        public int AlunoId { get; set; }
        public virtual required Aluno aluno { get; set; }//um aluno pode ter varias turmas
        public int TurmaId { get; set; }
        public virtual required Turma Turma { get; set; }//uma turma pode ter varios alunos
    }
}
