using Agenda.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Agenda.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Contexto _contexto;

        public UsuarioController(Contexto contexto)
        {
            _contexto = contexto;
        }
        /*[HttpGet]
                public async Task<IActionResult> Index()
                {
                    return View(await _contexto.Cadastro.ToListAsync());
                }*/

        //@(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier));

        [HttpGet]
        public IActionResult Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int id = int.Parse(user);

            if (id != null)
            {
                Usuario usuario = _contexto.UsuarioCadastro.Find(id);
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
                Usuario usuario = _contexto.UsuarioCadastro.Find(id);
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
                Usuario usuario = _contexto.UsuarioCadastro.Find(id);
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
