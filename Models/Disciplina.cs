using System.Text;

namespace pj_banco_quest.Models
{
    public class Disciplina
    {
        private string _nome = null!; // Usado internamente para armazenar o valor do nome
        private string _sigla = null!; // Usado internamente para armazenar o valor da sigla

        public int Id { get; set; }

        // Propriedade do nome com lógica de definição da sigla
        public required string Nome
        {
            get => _nome;
            set
            {
                _nome = value;
                _sigla = GerarSigla(_nome);
            }
        }

        public string Sigla => _sigla;

        // Método para gerar a sigla baseada no nome
        private static string GerarSigla(string nome)
        {
            var words = nome.Split(new[] { ' ', '-', '_' }, StringSplitOptions.RemoveEmptyEntries);
            var siglaBuilder = new StringBuilder();

            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    siglaBuilder.Append(char.ToUpper(word[0]));
                }
            }

            return siglaBuilder.ToString();
        }
    }
}
