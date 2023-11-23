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

        [HttpPost]
        public IActionResult AtualizarStatus(Dictionary<string, Dictionary<int, bool>> statusVacinas)
        {
            try
            {
                foreach (var categoria in statusVacinas)
                {
                    foreach (var vacinaIdAndStatus in categoria.Value)
                    {
                        int vacinaId = vacinaIdAndStatus.Key;
                        bool novoStatus = vacinaIdAndStatus.Value;

                        // Consulta o StatusVacina pelo Id da vacina
                        var statusVacina = _context.StatusVacinas.FirstOrDefault(sv => sv.Id == vacinaId);

                        if (statusVacina != null)
                        {
                            // Atualiza o status
                            statusVacina.Status = novoStatus;
                        }
                    }
                }

                // Salva as alterações no banco de dados
                _context.SaveChanges();

                TempData["msg"] = "Formulário Atualizado com Sucesso!";

                // Retorna uma resposta de sucesso
                return Ok();
            }
            catch (Exception ex)
            {
                // Captura qualquer exceção e imprime no console
                Console.WriteLine(ex.Message);
                return BadRequest("Erro ao atualizar status.");
            }
        }

        [HttpGet]
        public IActionResult Pendentes(int id)
        {
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null) { return NotFound(); }

            var listaStatusVacinas = _context.StatusVacinas
                .Where(s => s.UsuarioId == id)
                .Include(s => s.Vacina)
                .ToList();

            if (listaStatusVacinas == null)
            {
                return NotFound();
            }
            List<string> listaPendentes = new List<string>();

            if (!PendenteBCG(listaStatusVacinas))
            {
                listaPendentes.Add("BCG");
            }
            if (!PendenteHepatiteB(listaStatusVacinas))
            {
                listaPendentes.Add("Hepatite B");
            }
            if (!PendentePentavalente(listaStatusVacinas))
            {
                listaPendentes.Add("Pentavalente");
            }
            if (!PendentePoliomielite(listaStatusVacinas))
            {
                listaPendentes.Add("Poliomielite");
            }
            if (!PendentePneumococica(listaStatusVacinas))
            {
                listaPendentes.Add("Pneumocócica 10V");
            }
            if (!PendenteRotavirus(listaStatusVacinas))
            {
                listaPendentes.Add("Rotavírus");
            }
            if (!PendenteMeningococicaC(listaStatusVacinas))
            {
                listaPendentes.Add("Meningocócica C");
            }
            if (!PendenteFebreAmarela(listaStatusVacinas))
            {
                listaPendentes.Add("Febre Amarela");
            }
            if (!PendenteTripliceViral(listaStatusVacinas))
            {
                listaPendentes.Add("Tríplice Viral");
            }
            if (!PendenteHepatiteA(listaStatusVacinas))
            {
                listaPendentes.Add("Hepatite A");
            }
            if (!PendenteHPV(listaStatusVacinas))
            {
                listaPendentes.Add("HPV");
            }
            return View(listaPendentes);
        }

        public int ContagemVacinas(List<StatusVacina> listaStatusVacinas, TipoVacinaEnum tipo)
        {
            int contagem = 0;
            foreach (StatusVacina statusVacina in listaStatusVacinas)
            {
                if (statusVacina.Vacina.Tipo.Equals(tipo))
                {
                    if(statusVacina.Status.Equals(true))
                    {
                        contagem++;
                    }
                }
            }
            return contagem;
        }

        public bool PendenteBCG(List<StatusVacina> listaStatusVacina)
        {
            int contagemBCG = ContagemVacinas(listaStatusVacina, TipoVacinaEnum.BCG);
            if (contagemBCG == 0)
            {
                return false;
            }
            return true;
        }

        public bool PendenteHepatiteB(List<StatusVacina> listaStatusVacinas)
        {
            int contagemHepatiteB = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.HepatiteB);
            if (contagemHepatiteB == 0)
            {
                int contagemPentavalente = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.Pentavalente);
                if (contagemPentavalente < 5)
                {
                    return false;
                }
                return true;
            }
            return true;
        }

        public bool PendentePentavalente(List<StatusVacina> listaStatusVacinas)
        {
            int contagemPentavalente = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.Pentavalente);
            if (contagemPentavalente < 5)
            {
                int contagemAdolAdulIdo = 0;
                foreach (StatusVacina statusVacina in listaStatusVacinas)
                {
                    if ((statusVacina.Vacina.Categoria == CategoriaVacinaEnum.Adolescente ||
                         statusVacina.Vacina.Categoria == CategoriaVacinaEnum.Adulto ||
                         statusVacina.Vacina.Categoria == CategoriaVacinaEnum.Idoso) &&
                        statusVacina.Vacina.Tipo == TipoVacinaEnum.Pentavalente)
                    {
                        if (statusVacina.Status)
                        {
                            contagemAdolAdulIdo++;
                        }
                    }
                }
                if (contagemAdolAdulIdo < 2)
                {
                    return false;
                }
                return true;
            }
            return true;
        }

        public bool PendentePoliomielite(List<StatusVacina> listaStatusVacinas)
        {
            int contagemPoliomielite = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.Poliomielite);
            if (contagemPoliomielite < 5)
            {
                return false;
            }
            return true;
        }

        public bool PendentePneumococica(List<StatusVacina> listaStatusVacinas)
        {
            int contagemPneumococica = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.Pneumococica10V);
            if (contagemPneumococica < 3)
            {
                return false;
            }
            return true;
        }

        public bool PendenteRotavirus(List<StatusVacina> listaStatusVacinas)
        {
            int contagemRotavirus = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.Rotavirus);
            if (contagemRotavirus < 2)
            {
                return false;
            }
            return true;
        }

        public bool PendenteMeningococicaC(List<StatusVacina> listaStatusVacinas)
        {
            int contagemMeningococica = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.MeningococicaC);
            if (contagemMeningococica < 3)
            {
                if (listaStatusVacinas[0].Usuario.DataNascimento > DateTime.Now.AddYears(-10) && contagemMeningococica == 2)
                {
                    return true;
                }
                return false;
            }
            return true;
        }

        public bool PendenteFebreAmarela(List<StatusVacina> listaStatusVacinas)
        {
            int contagemFebreAmarela = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.FebreAmarela);
            if (contagemFebreAmarela == 0)
            {
                return false;
            }
            return true;
        }

        public bool PendenteTripliceViral(List<StatusVacina> listaStatusVacinas)
        {
            int contagemTripliceViral = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.TripliceViral);
            if (listaStatusVacinas[0].Usuario.DataNascimento > DateTime.Now.AddYears(-10) && contagemTripliceViral == 1)
            {
                return true;
            }
            else if (listaStatusVacinas[0].Usuario.DataNascimento > DateTime.Now.AddYears(-20) && contagemTripliceViral == 2)
            {
                return true;
            }
            else if (listaStatusVacinas[0].Usuario.DataNascimento > DateTime.Now.AddYears(-50) && contagemTripliceViral != 0)
            {
                return true;
            }
            return false;
        }

        public bool PendenteHepatiteA(List<StatusVacina> listaStatusVacinas)
        {
            int contagemHepatiteA = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.HepatiteA);
            if (contagemHepatiteA == 0)
            {
                return false;
            }
            return true;
        }

        public bool PendenteTetraViral(List<StatusVacina> listaStatusVacinas)
        {
            int contagemTetraViral = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.TetraViral);
            if (contagemTetraViral == 0)
            {
                return false;
            }
            return true;
        }

        public bool PendenteHPV(List<StatusVacina> listaStatusVacinas)
        {
            int contagemHPV = ContagemVacinas(listaStatusVacinas, TipoVacinaEnum.HPV);
            if (listaStatusVacinas[0].Usuario.DataNascimento > DateTime.Now.AddYears(-10) || contagemHPV > 1)
            {
                return true;
            }
            return false;
        }


    }

}