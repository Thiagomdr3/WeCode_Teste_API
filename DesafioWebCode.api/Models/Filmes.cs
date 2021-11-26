using System.ComponentModel.DataAnnotations;

namespace DesafioWebCode.api.Models
{
    public class Filmes
    {
        [Key()]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Genero { get; set; }
        public int Classificacao { get; set; }
    }
}
