using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioWebCode.api.Models
{
    public class Assistidos
    {
        [Key()]
        public int Id { get; set; }

        [ForeignKey("Pessoas")]
        public int PessoasId { get; set; }
        public virtual Pessoas Pessoas { get; set; }

        [ForeignKey("Filmes")]
        public int FilmesId { get; set; }
        public virtual Filmes Filmes { get; set; }
    }
}
