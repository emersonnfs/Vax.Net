using System.ComponentModel.DataAnnotations;

namespace Vax.Models
{
    public class StatusVacina
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O status da vacina é obrigatório")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "O id do usuário é obrigatório")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        [Required(ErrorMessage = "O id da vacina é obrigatório")]
        public int VacinaId { get; set; }

        public Vacina Vacina { get; set; }
    }
}
