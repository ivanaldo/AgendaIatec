using Agenda.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
     
        //@(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier));
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? id = int.Parse(user);

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
            CarregaTipoSexo();
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
                CarregaTipoSexo();
                return View(usuario);
              }
        }
        [Authorize]
        [HttpGet]
        public IActionResult AtualizarUsuario(int? id)
        {
            if (id != null)
            {
                CarregaTipoSexo();
                Usuario usuario = _contexto.UsuarioCadastro.Find(id);
                return View(usuario);
            }
            else
            {
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AtualizarUsuario(int? id, Usuario usuario)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Update(usuario);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("Index", "Usuario", new { id = id });
                }
                else
                {
                    return View(usuario);
                }
            }
            else
            {
                CarregaTipoSexo();
                return NotFound();
            }
        }
        [Authorize]
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ExcluirUsuario(int? id, Usuario usuario)
        {
            if (id != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    await HttpContext.SignOutAsync();
                }
                _contexto.Remove(usuario);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("LoginUsuario", "Login");
            }
            else
            {
                return NotFound();
            }
        }

        public void CarregaTipoSexo()
        {
            var ItemSexo = new List<SelectListItem>
            {
                new SelectListItem{Value = "Masculino", Text = "Masculino"},
                new SelectListItem{Value = "Feminino", Text = "Feminino"}
            };

            ViewBag.TipoSexo = ItemSexo;
        }
    }
}
