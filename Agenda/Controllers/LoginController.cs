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
        public IActionResult LoginUsuario()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Evento");
            }
               
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginUsuario (string email, string senha)
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=CARVALHO\\SQLEXPRESS;Initial Catalog=AgendaIatec;Integrated Security=True;");
            await sqlConnection.OpenAsync();

            SqlCommand SqlCommand = sqlConnection.CreateCommand();
            SqlCommand.CommandText = $"SELECT * FROM UsuarioCadastro WHERE email = '{email}' AND senha = '{senha}'";

            //retornar os dados do usuário cadastrados no banco
            SqlDataReader reader = SqlCommand.ExecuteReader();


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
                
                return RedirectToAction("Index", "Evento", new { id = usuarioId});
                
                }
              
            return RedirectToAction("LoginUsuario", "Login");

        }

        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }

            return RedirectToAction("LoginUsuario", "Login");
        }

    }
}
