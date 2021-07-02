using AgendaIAtec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AgendaIAtec.Controllers
{
    public class CadastroController : Controller
    {
        private readonly Contexto _contexto;

        public CadastroController(Contexto contexto)
        {
            _contexto = contexto;
        }

        /*[HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _contexto.Cadastro.ToListAsync());
        }*/

        [HttpGet]
        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                Usuario usuario = _contexto.Cadastro.Find(id);
                return View(usuario);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult CriarUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
               
                _contexto.Add(usuario);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("LoginUsuario", "Login");
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpGet]
        public IActionResult AtualizarUsuario(int? id)
        {
            if (id != null)
            {
                Usuario usuario = _contexto.Cadastro.Find(id);
                return View(usuario);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarUsuario(int? id, Usuario usuario)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Update(usuario);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("Index", "Cadastro", new { id = id });
                }
                else
                {
                    return View(usuario);
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult ExcluirUsuario(int? id)
        {
            if (id != null)
            {
                Usuario usuario = _contexto.Cadastro.Find(id);
                return View(usuario);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirUsuario(int? id, Usuario usuario)
        {
            if (id != null)
            {
                _contexto.Remove(usuario);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("LoginUsuario", "Login");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
