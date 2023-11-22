using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Vax.Data;
using Vax.Models;

namespace Vax.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var usuarios = _context.Usuarios
                .Include(u => u.Endereco)
                .Include(u => u.Telefone)
                .ToList();

            return View(usuarios);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                int enderecoId = CadastrarEndereco(usuario.Endereco);
                int telefoneId = CadastrarTelefone(usuario.Telefone);

                if (enderecoId != 0 && telefoneId != 0)
                {
                    usuario.EnderecoId = enderecoId;
                    usuario.TelefoneId = telefoneId;

                    _context.Usuarios.Add(usuario);
                    _context.SaveChanges();

                    if (usuario.Genero == GeneroEnum.Feminino)
                    {
                        for (int vacinaId = 1; vacinaId <= 48; vacinaId++)
                        {
                            StatusVacina statusVacina = new StatusVacina
                            {
                                Status = false,
                                UsuarioId = usuario.Id,
                                VacinaId = vacinaId
                            };

                            _context.StatusVacinas.Add(statusVacina);
                        }

                        _context.SaveChanges();
                    }
                    else {
                        for (int vacinaId = 1; vacinaId <= 47; vacinaId++)
                        {
                            StatusVacina statusVacina = new StatusVacina
                            {
                                Status = false,
                                UsuarioId = usuario.Id,
                                VacinaId = vacinaId
                            };

                            _context.StatusVacinas.Add(statusVacina);
                        }

                        _context.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
            }

            return View(usuario);
        }


        private int CadastrarEndereco(Endereco endereco)
        {
            if (endereco != null)
            {
                _context.Enderecos.Add(endereco);
                _context.SaveChanges();
                return endereco.Id;
            }

            return 0;
        }

        private int CadastrarTelefone(Telefone telefone)
        {
            if (telefone != null)
            {
                _context.Telefones.Add(telefone);
                _context.SaveChanges();
                return telefone.Id;
            }

            return 0;
        }
        

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Endereco)
                .Include(u => u.Telefone)
                .FirstOrDefault(u => u.Id == id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(Usuario usuario)
        {
            var usuarioOriginal = _context.Usuarios
                .Include(u => u.Endereco)
                .Include(u => u.Telefone)
                .FirstOrDefault(u => u.Id == usuario.Id);

            if (usuarioOriginal == null)
            {
                return NotFound();
            }

            usuarioOriginal.Nome = usuario.Nome;
            usuarioOriginal.Email = usuario.Email;
            usuarioOriginal.DataNascimento = usuario.DataNascimento;
            usuarioOriginal.Genero = usuario.Genero;

            usuarioOriginal.Endereco.Estado = usuario.Endereco.Estado;
            usuarioOriginal.Endereco.Cidade = usuario.Endereco.Cidade;
            usuarioOriginal.Endereco.Logradouro = usuario.Endereco.Logradouro;

            usuarioOriginal.Telefone.Ddd = usuario.Telefone.Ddd;
            usuarioOriginal.Telefone.Numero = usuario.Telefone.Numero;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Remover(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if(usuario == null) {  return NotFound(); }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            TempData["msg"] = "Usuário removido!";
            return RedirectToAction("Index");
        }
    }
}
