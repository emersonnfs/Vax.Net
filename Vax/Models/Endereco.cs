using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Vax.Models
{
    public class Endereco
    {
        [HiddenInput]
        public int Id { get; set; }
        [Required(ErrorMessage = "O Estado é obrigatório.")]
        public EstadoEnum Estado { get; set; }
        [Required(ErrorMessage = "A cidade é obrigatório.")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "O Logradouro é obrigatório.")]
        public string Logradouro { get; set;}
    }

    public enum EstadoEnum
    {
        AC, // Acre
        AL, // Alagoas
        AP, // Amapá
        AM, // Amazonas
        BA, // Bahia
        CE, // Ceará
        DF, // Distrito Federal
        ES, // Espírito Santo
        GO, // Goiás
        MA, // Maranhão
        MT, // Mato Grosso
        MS, // Mato Grosso do Sul
        MG, // Minas Gerais
        PA, // Pará
        PB, // Paraíba
        PR, // Paraná
        PE, // Pernambuco
        PI, // Piauí
        RJ, // Rio de Janeiro
        RN, // Rio Grande do Norte
        RS, // Rio Grande do Sul
        RO, // Rondônia
        RR, // Roraima
        SC, // Santa Catarina
        SP, // São Paulo
        SE, // Sergipe
        TO  // Tocantins
    }
}