using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Vax.Data;
using Vax.Models;

namespace Vax.Services
{
    public class DataInitializationService
    {
        private readonly IServiceProvider _serviceProvider;

        public DataInitializationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void InitializeData()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                if (!dbContext.Vacinas.Any())
                {
                    //Vacina BCG
                    var vacina1 = new Vacina 
                    { 
                        Id = 1, 
                        Nome = "BCG", 
                        Tipo = TipoVacinaEnum.BCG, 
                        Categoria = CategoriaVacinaEnum.Criança, 
                        Dose = DoseVacinaEnum.DoseUnica 
                    };
                    dbContext.Vacinas.Add(vacina1);

                    //Vacinas Hepatite B
                    var vacina2 = new Vacina
                    {
                        Id = 2,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina2);

                    var vacina3 = new Vacina
                    {
                        Id = 3,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina3);

                    var vacina4 = new Vacina
                    {
                        Id = 4,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina4);

                    var vacina5 = new Vacina
                    {
                        Id = 5,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.TerceiraDose
                    };
                    dbContext.Vacinas.Add(vacina5);

                    var vacina6 = new Vacina
                    {
                        Id = 6,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Adulto,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina6);

                    var vacina7 = new Vacina
                    {
                        Id = 7,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Adulto,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina7);

                    var vacina8 = new Vacina
                    {
                        Id = 8,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Adulto,
                        Dose = DoseVacinaEnum.TerceiraDose
                    };
                    dbContext.Vacinas.Add(vacina8);

                    var vacina9 = new Vacina
                    {
                        Id = 9,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Idoso,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina9);

                    var vacina10 = new Vacina
                    {
                        Id = 10,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Idoso,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina10);

                    var vacina11 = new Vacina
                    {
                        Id = 11,
                        Nome = "Hepatite B",
                        Tipo = TipoVacinaEnum.HepatiteB,
                        Categoria = CategoriaVacinaEnum.Idoso,
                        Dose = DoseVacinaEnum.TerceiraDose
                    };
                    dbContext.Vacinas.Add(vacina11);

                    //Vacinas Pentavalentes
                    var vacina12 = new Vacina
                    {
                        Id = 12,
                        Nome = "Pentavalente (DTP/HB/Hib)",
                        Tipo = TipoVacinaEnum.Pentavalente,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina12);

                    var vacina13 = new Vacina
                    {
                        Id = 13,
                        Nome = "Pentavalente (DTP/HB/Hib)",
                        Tipo = TipoVacinaEnum.Pentavalente,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina13);

                    var vacina14 = new Vacina
                    {
                        Id = 14,
                        Nome = "Pentavalente (DTP/HB/Hib)",
                        Tipo = TipoVacinaEnum.Pentavalente,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.TerceiraDose
                    };
                    dbContext.Vacinas.Add(vacina14);

                    var vacina15 = new Vacina
                    {
                        Id = 15,
                        Nome = "DTP",
                        Tipo = TipoVacinaEnum.Pentavalente,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiroReforco
                    };
                    dbContext.Vacinas.Add(vacina15);

                    var vacina16 = new Vacina
                    {
                        Id = 16,
                        Nome = "DTP",
                        Tipo = TipoVacinaEnum.Pentavalente,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.SegundoReforco
                    };
                    dbContext.Vacinas.Add(vacina16);

                    //Vacinas Poliomielite
                    var vacina17 = new Vacina
                    {
                        Id = 17,
                        Nome = "VIP (Poliomielite inativada)",
                        Tipo = TipoVacinaEnum.Poliomielite,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina17);

                    var vacina18 = new Vacina
                    {
                        Id = 18,
                        Nome = "VIP (Poliomielite inativada)",
                        Tipo = TipoVacinaEnum.Poliomielite,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina18);

                    var vacina19 = new Vacina
                    {
                        Id = 19,
                        Nome = "VIP (Poliomielite inativada)",
                        Tipo = TipoVacinaEnum.Poliomielite,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.TerceiraDose
                    };
                    dbContext.Vacinas.Add(vacina19);

                    var vacina20 = new Vacina
                    {
                        Id = 20,
                        Nome = "Poliomielite oral VOP",
                        Tipo = TipoVacinaEnum.Poliomielite,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiroReforco
                    };
                    dbContext.Vacinas.Add(vacina20);

                    var vacina21 = new Vacina
                    {
                        Id = 21,
                        Nome = "Poliomielite oral VOP",
                        Tipo = TipoVacinaEnum.Poliomielite,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.SegundoReforco
                    };
                    dbContext.Vacinas.Add(vacina21);

                    //Vacinas Pneumocócica 10V
                    var vacina22 = new Vacina
                    {
                        Id = 22,
                        Nome = "Pneumocócica 10V",
                        Tipo = TipoVacinaEnum.Pneumococica10V,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina22);

                    var vacina23 = new Vacina
                    {
                        Id = 23,
                        Nome = "Pneumocócica 10V",
                        Tipo = TipoVacinaEnum.Pneumococica10V,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina23);

                    var vacina24 = new Vacina
                    {
                        Id = 24,
                        Nome = "Pneumocócica 10V",
                        Tipo = TipoVacinaEnum.Pneumococica10V,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiroReforco
                    };
                    dbContext.Vacinas.Add(vacina24);

                    //Vacinas do Rotavirus
                    var vacina25 = new Vacina
                    {
                        Id = 25,
                        Nome = "Vacina rotavírus humano G1P1",
                        Tipo = TipoVacinaEnum.Rotavirus,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina25);

                    var vacina26 = new Vacina
                    {
                        Id = 26,
                        Nome = "Vacina rotavírus humano G1P1",
                        Tipo = TipoVacinaEnum.Rotavirus,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina26);

                    //Vacinas Monigácia C
                    var vacina27 = new Vacina
                    {
                        Id = 27,
                        Nome = "Meningocócica C conjugada",
                        Tipo = TipoVacinaEnum.MeningococicaC,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina27);

                    var vacina28 = new Vacina
                    {
                        Id = 28,
                        Nome = "Meningocócica C conjugada",
                        Tipo = TipoVacinaEnum.MeningococicaC,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina28);

                    var vacina29 = new Vacina
                    {
                        Id = 29,
                        Nome = "Meningocócica C conjugada",
                        Tipo = TipoVacinaEnum.MeningococicaC,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.DoseUnica
                    };
                    dbContext.Vacinas.Add(vacina29);

                    //Vacinas Febre Amarela
                    var vacina30 = new Vacina
                    {
                        Id = 30,
                        Nome = "Febre amarela",
                        Tipo = TipoVacinaEnum.FebreAmarela,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.DoseUnica
                    };
                    dbContext.Vacinas.Add(vacina30);

                    var vacina31 = new Vacina
                    {
                        Id = 31,
                        Nome = "Febre amarela",
                        Tipo = TipoVacinaEnum.FebreAmarela,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.DoseUnica
                    };
                    dbContext.Vacinas.Add(vacina31);

                    var vacina32 = new Vacina
                    {
                        Id = 32,
                        Nome = "Febre amarela",
                        Tipo = TipoVacinaEnum.FebreAmarela,
                        Categoria = CategoriaVacinaEnum.Adulto,
                        Dose = DoseVacinaEnum.DoseUnica
                    };
                    dbContext.Vacinas.Add(vacina32);

                    var vacina33 = new Vacina
                    {
                        Id = 33,
                        Nome = "Febre amarela",
                        Tipo = TipoVacinaEnum.FebreAmarela,
                        Categoria = CategoriaVacinaEnum.Idoso,
                        Dose = DoseVacinaEnum.DoseUnica
                    };
                    dbContext.Vacinas.Add(vacina33);

                    //Vacinas Triplice Viral
                    var vacina34 = new Vacina
                    {
                        Id = 34 ,
                        Nome = "Tríplice viral (sarampo, caxumba e rubéola)",
                        Tipo = TipoVacinaEnum.TripliceViral,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina34);

                    var vacina35 = new Vacina
                    {
                        Id = 35,
                        Nome = "Tríplice viral (sarampo, caxumba e rubéola)",
                        Tipo = TipoVacinaEnum.TripliceViral,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina35);

                    var vacina36 = new Vacina
                    {
                        Id = 36,
                        Nome = "Tríplice viral (sarampo, caxumba e rubéola)",
                        Tipo = TipoVacinaEnum.TripliceViral,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina36);

                    var vacina37 = new Vacina
                    {
                        Id = 37,
                        Nome = "Tríplice viral (sarampo, caxumba e rubéola)",
                        Tipo = TipoVacinaEnum.TripliceViral,
                        Categoria = CategoriaVacinaEnum.Idoso,
                        Dose = DoseVacinaEnum.DoseUnica
                    };
                    dbContext.Vacinas.Add(vacina37);

                    //Vacinas Hepatite A
                    var vacina38 = new Vacina
                    {
                        Id = 38,
                        Nome = "Hepatite A",
                        Tipo = TipoVacinaEnum.HepatiteA,
                        Categoria = CategoriaVacinaEnum.Criança,
                        Dose = DoseVacinaEnum.DoseUnica
                    };
                    dbContext.Vacinas.Add(vacina38);

                    //Vacinas HPV
                    var vacina39 = new Vacina
                    {
                        Id = 39,
                        Nome = "HPV",
                        Tipo = TipoVacinaEnum.HPV,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina39);
  
                    var vacina40 = new Vacina
                    {
                        Id = 40,
                        Nome = "HPV",
                        Tipo = TipoVacinaEnum.HPV,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina40);

                    var vacina41 = new Vacina
                    {
                        Id = 41,
                        Nome = "HPV",
                        Tipo = TipoVacinaEnum.HPV,
                        Categoria = CategoriaVacinaEnum.Adulto,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina41);

                    var vacina42 = new Vacina
                    {
                        Id = 42,
                        Nome = "HPV",
                        Tipo = TipoVacinaEnum.HPV,
                        Categoria = CategoriaVacinaEnum.Adulto,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina42);

                    var vacina43 = new Vacina
                    {
                        Id = 43,
                        Nome = "HPV",
                        Tipo = TipoVacinaEnum.HPV,
                        Categoria = CategoriaVacinaEnum.Idoso,
                        Dose = DoseVacinaEnum.PrimeiraDose
                    };
                    dbContext.Vacinas.Add(vacina43);

                    var vacina44 = new Vacina
                    {
                        Id = 44,
                        Nome = "HPV",
                        Tipo = TipoVacinaEnum.HPV,
                        Categoria = CategoriaVacinaEnum.Idoso,
                        Dose = DoseVacinaEnum.SegundaDose
                    };
                    dbContext.Vacinas.Add(vacina44);

                    //Vacinas Duplo Adulto
                    var vacina45 = new Vacina
                    {
                        Id = 45,
                        Nome = "Dupla Adulto (dT)",
                        Tipo = TipoVacinaEnum.DuplaAdulto,
                        Categoria = CategoriaVacinaEnum.Adolescente,
                        Dose = DoseVacinaEnum.Periodica
                    };
                    dbContext.Vacinas.Add(vacina45);

                    var vacina46 = new Vacina
                    {
                        Id = 46,
                        Nome = "Dupla Adulto (dT)",
                        Tipo = TipoVacinaEnum.DuplaAdulto,
                        Categoria = CategoriaVacinaEnum.Adulto,
                        Dose = DoseVacinaEnum.Periodica
                    };
                    dbContext.Vacinas.Add(vacina46);

                    var vacina47 = new Vacina
                    {
                        Id = 47,
                        Nome = "Dupla Adulto (dT)",
                        Tipo = TipoVacinaEnum.DuplaAdulto,
                        Categoria = CategoriaVacinaEnum.Idoso,
                        Dose = DoseVacinaEnum.Periodica
                    };
                    dbContext.Vacinas.Add(vacina47);

                    //Vacinas dTPa
                    var vacina48 = new Vacina
                    {
                        Id = 48,
                        Nome = "dTPa (Tríplice bacteriana acelular do tipo adulto)",
                        Tipo = TipoVacinaEnum.dTPa,
                        Categoria = CategoriaVacinaEnum.Gestante,
                        Dose = DoseVacinaEnum.DoseUnica
                    };
                    dbContext.Vacinas.Add(vacina48);

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
