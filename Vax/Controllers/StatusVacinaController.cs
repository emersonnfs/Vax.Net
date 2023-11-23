using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Vax.Data;
using Vax.Models;

namespace Vax.Controllers
{
    public class StatusVacinaController : Controller
    {
        private readonly DataContext _context;

        public StatusVacinaController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Formulario(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            var listaStatusVacinas = _context.StatusVacinas
                .Where(s => s.UsuarioId == id)
                .Include(s => s.Vacina)
                .ToList();

            var mapaCategorias = new Dictionary<string, Dictionary<string, List<object>>>();

            foreach (var categoriaVacinaEnum in Enum.GetValues(typeof(CategoriaVacinaEnum)).OfType<CategoriaVacinaEnum>())
            {
                var mapaTipos = ListarVacinas(listaStatusVacinas, categoriaVacinaEnum);
                if (mapaTipos.Any())
                {
                    var listaTipos = mapaTipos.ToDictionary(
                        kvp => kvp.Key.ToString(),
                        kvp => kvp.Value
                            .Select(vacina => new FormVacina
                            {
                                Id = vacina.Id,
                                Nome = vacina.Nome,
                                Descricao = vacina.Descricao,
                                Dose = vacina.Dose,
                                Status = vacina.Status
                            })
                            .ToList<object>()
                    );

                    mapaCategorias.Add(categoriaVacinaEnum.ToString(), listaTipos);
                }
            }

            var hoje = DateTime.Now;
            var idade = hoje.Year - usuario.DataNascimento.Year;

            if (idade >= 60)
            {
                mapaCategorias.Remove(CategoriaVacinaEnum.Gestante.ToString());
                return View(mapaCategorias);
            }
            else if (idade >= 20)
            {
                mapaCategorias.Remove(CategoriaVacinaEnum.Idoso.ToString());
                mapaCategorias.Remove(CategoriaVacinaEnum.Gestante.ToString());
                return View(mapaCategorias);
            }
            else if (idade >= 11)
            {
                mapaCategorias.Remove(CategoriaVacinaEnum.Idoso.ToString());
                mapaCategorias.Remove(CategoriaVacinaEnum.Adulto.ToString());
                mapaCategorias.Remove(CategoriaVacinaEnum.Gestante.ToString());
                return View(mapaCategorias);
            }
            else if (idade >= 0)
            {
                mapaCategorias.Remove(CategoriaVacinaEnum.Idoso.ToString());
                mapaCategorias.Remove(CategoriaVacinaEnum.Adulto.ToString());
                mapaCategorias.Remove(CategoriaVacinaEnum.Adolescente.ToString());
                mapaCategorias.Remove(CategoriaVacinaEnum.Gestante.ToString());
                return View(mapaCategorias);
            }
            else
            {
                return BadRequest();
            }
        }

        public Dictionary<TipoVacinaEnum, List<FormVacina>> ListarVacinas(List<StatusVacina> statusVacinas, CategoriaVacinaEnum categoriaVacinaEnum)
        {
            Dictionary<TipoVacinaEnum, List<FormVacina>> mapaVacinas = new Dictionary<TipoVacinaEnum, List<FormVacina>>();

            foreach (var statusVacina in statusVacinas)
            {
                var vacina = statusVacina.Vacina;
                var categoriaVacina = vacina.Categoria;

                if (categoriaVacina == categoriaVacinaEnum)
                {
                    var tipoVacina = vacina.Tipo;
                    var descricaoVacina = ObterDescricaoVacina(tipoVacina);

                    var formVacina = new FormVacina
                    {
                        Id = statusVacina.Id,
                        Nome = descricaoVacina.Nome,
                        Descricao = descricaoVacina.Descricao,
                        Dose = vacina.Dose,
                        Status = statusVacina.Status
                    };

                    if (!mapaVacinas.ContainsKey(tipoVacina))
                    {
                        mapaVacinas[tipoVacina] = new List<FormVacina>();
                    }

                    mapaVacinas[tipoVacina].Add(formVacina);
                }
            }

            return mapaVacinas;
        }

        private DescricaoVacinaAttribute ObterDescricaoVacina(TipoVacinaEnum vacinaEnum)
        {
            try
            {
                var fieldInfo = typeof(TipoVacinaEnum).GetField(vacinaEnum.ToString());
                var descricaoVacina = (DescricaoVacinaAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescricaoVacinaAttribute));

                return descricaoVacina;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }


    }

}