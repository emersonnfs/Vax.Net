using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Vax.Models
{
    public class Telefone
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage ="O DDD do telefone é obrigatório.")]
        public int Ddd { get; set; }
        [Required(ErrorMessage ="O número do telefone é obrigatório.")]
        public int Numero { get; set; }
    }
}
