using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vax.Models
{
    public class Usuario
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email do usuário é obrigatório.")]
        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "A data de nascimento do usuário é obrigatório.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Gênero biológico do Usuário")]
        [Required(ErrorMessage = "O gênero do usuário é obrigatório.")]
        public GeneroEnum Genero { get; set; }
        public int? EnderecoId { get; set; }
        public Endereco Endereco { get; set; }
        public int? TelefoneId { get; set; }
        public Telefone Telefone { get; set; }

        public ICollection<StatusVacina>? StatusVacinas { get; set; }
    }

    public enum GeneroEnum
    {
        Masculino,
        Feminino,
        Outro
    }
}
