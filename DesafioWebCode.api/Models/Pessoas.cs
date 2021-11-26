using System.ComponentModel.DataAnnotations;

namespace DesafioWebCode.api.Models
{
    public class Pessoas
    {
        [Key()]
        public int Id { get; set; }
        public string Nome { get; set;}
        public string Email { get; set;}
    }
}
