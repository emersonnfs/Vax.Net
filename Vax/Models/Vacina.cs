namespace Vax.Models
{
    public class Vacina
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoVacinaEnum Tipo { get; set; }
        public CategoriaVacinaEnum Categoria { get; set; }
        public DoseVacinaEnum Dose { get; set; }
        public ICollection<StatusVacina>? StatusVacinas   { get; set; }
    }

    public enum CategoriaVacinaEnum
    {
        Criança, 
        Adolescente, 
        Adulto, 
        Gestante, 
        Idoso
    }

    public enum DoseVacinaEnum
    {
        PrimeiraDose, 
        SegundaDose, 
        TerceiraDose, 
        DoseUnica, 
        PrimeiroReforco, 
        SegundoReforco, 
        Periodica
    }
}
