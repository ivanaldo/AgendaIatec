using Agenda.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Agenda.Controllers
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
            {
                return RedirectToAction("Index", "Home");
            }
               
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUsuario (string email, string senha)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=CARVALHO\\SQLEXPRESS;Initial Catalog=AgendaIatec;Integrated Security=True;");
            await sqlConnection.OpenAsync();

            SqlCommand mySqlCommand = sqlConnection.CreateCommand();
            mySqlCommand.CommandText = $"SELECT * FROM UsuarioCadastro WHERE email = '{email}' AND senha = '{senha}'";

            //retornar os dados do usuário cadastrados no banco
            SqlDataReader reader = mySqlCommand.ExecuteReader();


            //verifica se tem algum usuário
                if(await reader.ReadAsync())
                {
                //resgatando o nome e o id do usuario no campo do BD
                int usuarioId = reader.GetInt32(0);
                string nome = reader.GetString(1);

                //defini os direitos de acesso do usuário
                List<Claim> direitoAcesso = new List<Claim>
                {
                    //defini o nome e o id do usuário logado
                    new Claim(ClaimTypes.NameIdentifier, usuarioId.ToString()),
                    new Claim(ClaimTypes.Name, nome)
                };

                //salva os direito de acesso
                var identity = new ClaimsIdentity(direitoAcesso, "identity.login");
                var userPrincipal = new ClaimsPrincipal(new[] { identity });
                 //loga usuário
                await HttpContext.SignInAsync(userPrincipal, 
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddHours(1)
                    });
                
                return RedirectToAction("Index", "Home", new { id = usuarioId});
                }

            return RedirectToAction("LoginUsuario", "Login");

        }

        public async Task<IActionResult> logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

            return RedirectToAction("LoginUsuario", "Login");
        }

    }
}
/*[HttpPost]
        public async Task<IActionResult> LoginUsuario (Usuario login)
        {
            if (login != null)
            {
                if ( _contexto.UsuarioCadastro.Any(a => a.email == login.email
                 && a.senha == login.senha))
                {
                    int id = _contexto.UsuarioCadastro.Where(u => u.email == login.email
                    && u.senha == login.senha).Select(u => u.usuarioId).Single();

                    HttpContext.Session.SetInt32("usuarioId", id);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, login.email)
                    };

                    var userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction("Index", "Usuario", new { id = id });
                }
                else
                {
                    return RedirectToAction("LoginUsuario", "Login");

                }
            }

            return View(login);

        }*/
