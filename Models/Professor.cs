namespace pj_banco_quest.Models
{
    public class Professor : Usuario
    {
        public List<Turma> Turmas { get; set; } = new List<Turma>();
        public required string IdFuncional { get; set; }
    }
}