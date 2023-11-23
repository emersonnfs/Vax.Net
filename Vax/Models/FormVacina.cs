using System.Drawing;

namespace Vax.Models
{
    public class FormVacina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DoseVacinaEnum Dose { get; set; }
        public bool Status {  get; set; } 
    }
    public class CategoriaVacinaViewModel
    {
        public string CategoriaNome { get; set; }
        public Dictionary<string, List<FormVacina>> Tipos { get; set; }
    }
}
