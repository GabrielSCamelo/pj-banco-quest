namespace pj_banco_quest.Models
{
    public class Simulado_Aluno
    {
        public int SimuladoId { get; set; }
        public virtual required Simulado Simulado { get; set; }
        public int AlunoId { get; set; }
        public virtual required Aluno Aluno { get; set; }
        public List<char> Respostas { get; set; } = new List<char> { ' ' };
        public int TotalPontos { get; set; }
    }
}