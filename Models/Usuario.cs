using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace pj_banco_quest.Models
{
    public abstract class Usuario
    {
        public int Id { get; set; }

        public required string UserId { get; set; }
        public virtual required IdentityUser User { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Senha { get; set; }
        public List<int> SimuladoCriado { get; set; } = new List<int>();
    }
}