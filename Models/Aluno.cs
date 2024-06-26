using System.ComponentModel.DataAnnotations;

namespace pj_banco_quest.Models
{
    public class Aluno : Usuario
    {
        public required List<Aluno_Turma> Turmas { get; set; }
        public required List<Simulado_Aluno> Simulados { get; set; }

        [Required(ErrorMessage = "O campo Matrícula é obrigatório.")]
        [MaxLength(50, ErrorMessage = "A matrícula deve ter no máximo 50 caracteres.")]
        public required string Matricula { get; set; }
    }
}