namespace Vax.Models
{
    public enum TipoVacinaEnum
    {
        [DescricaoVacina("BCG (Bacilo Calmette-Guerin)", "Previne as formas graves de tuberculose, principalmente miliar e meníngea.")]
        BCG,

        [DescricaoVacina("Hepatite B", "Previne a hepatite B.")]
        HepatiteB,

        [DescricaoVacina("Pentavalente (DTP/HB/Hib)", "Previne difteria, tétano, coqueluche, hepatite B e meningite e infecções por HiB.")]
        Pentavalente,

        [DescricaoVacina("Poliomielite", "Previne poliomielite ou paralisia infantil.")]
        Poliomielite,

        [DescricaoVacina("Pneumocócica 10V", "Previne pneumonia, otite, meningite e outras doenças causadas pelo Pneumococo.")]
        Pneumococica10V,

        [DescricaoVacina("Vacina rotavírus humano G1P1", "Previne diarreia por rotavírus.")]
        Rotavirus,

        [DescricaoVacina("Meningocócica C conjugada", "Previne a doença meningocócica C.")]
        MeningococicaC,

        [DescricaoVacina("Febre amarela", "Previne a febre amarela.")]
        FebreAmarela,

        [DescricaoVacina("Tríplice viral (sarampo, caxumba e rubéola)", "Previne sarampo, caxumba e rubéola.")]
        TripliceViral,

        [DescricaoVacina("Hepatite A", "Previne a hepatite A.")]
        HepatiteA,

        [DescricaoVacina("Tetra viral ou tríplice viral + varicela", "Previne sarampo, rubéola, caxumba e varicela/catapora.")]
        TetraViral,

        [DescricaoVacina("HPV", "Previne o papiloma, vírus humano que causa cânceres e verrugas genitais.")]
        HPV,

        [DescricaoVacina("Dupla Adulto (dT)", "Previne difteria e tétano.")]
        DuplaAdulto,

        [DescricaoVacina("dTPa (Tríplice bacteriana acelular do tipo adulto)", "Previne difteria, tétano e coqueluche. Uma dose a cada gestação a partir da 20ª semana de gestação ou no puerpério (até 45 dias após o parto).")]
        dTPa
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class DescricaoVacinaAttribute : Attribute
    {
        public string Nome { get; }
        public string Descricao { get; }

        public DescricaoVacinaAttribute(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
    }
}
