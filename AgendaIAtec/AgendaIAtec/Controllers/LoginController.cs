using AgendaIAtec.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AgendaIAtec.Controllers
{
    public class LoginController : Controller
    {
   
            private readonly Contexto _contexto;

            public LoginController(Contexto contexto)
            {
                _contexto = contexto;
            }

       public IActionResult LoginUsuario()
        {
            if (User.Identity.IsAuthenticated)
                HttpContext.Session.Clear();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUsuario(Usuario login)
        {
            if (login != null)
            {
                if(_contexto.Cadastro.Any(a => a.email == login.email 
                && a.senha == login.senha))
                    {
                        int id = _contexto.Cadastro.Where(u => u.email == login.email
                        && u.senha == login.senha).Select(u => u.usuarioId).Single();

                        /*HttpContext.Session.SetInt32("usuarioId", id);

                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, login.email)
                        };

                        var userIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                        await HttpContext.SignInAsync(principal);*/

                    return RedirectToAction("Index", "Cadastro", new {id = id });
                }
                else {
                    return RedirectToAction("LoginUsuario", "Login");
                   
                }
            }
            
            return View(login);

        }
    }
}
