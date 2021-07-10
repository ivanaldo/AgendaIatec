using Agenda.Consultas;
using Agenda.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Agenda.Controllers
{
    [Authorize]
    public class EventoController : Controller
    {
        private readonly Contexto _contexto;

        public EventoController(Contexto contexto)
        {
            _contexto = contexto;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        { 
        //retorna o id do usuário
        var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        int? idusuario = int.Parse(user); 

        ConsultasEvento con = new ConsultasEvento();

            List<Evento> list = await con.BuscaPorNome(idusuario, "Jogar bola");

            return View(list);

        }

        [HttpGet]
        public async Task<IActionResult> MeusEventos()
        {
            //retorna o id do usuário
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? idusuario = int.Parse(user);

            ConsultasEvento con = new ConsultasEvento();

            List<Evento> list = await con.MeusEventos(idusuario);

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Proximo()
        {

            //retorna o id do usuário
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? idusuario = int.Parse(user);

            ConsultasEvento conProximo = new ();

            List<Evento> list = await conProximo.ProximoEvento(idusuario);

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> EventoExclusivo()
        {
            //retorna o id do usuário
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? idusuario = int.Parse(user);

            ConsultasEvento conProximo = new();

            List<Evento> list = await conProximo.Exclusivo(idusuario);

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> EventoCompartilhado()
        {
            //retorna o id do usuário
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? idusuario = int.Parse(user);

            ConsultasEvento conProximo = new();

            List<Evento> list = await conProximo.Compartilhado(idusuario);

            return View(list);
        }

        [HttpGet]
        public IActionResult DetalhesEvento(int? id)
        {

            if (id != null)
            {
                Evento evento = _contexto.Evento.Find(id);
                return View(evento);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult CriarEvento()
        {
            CarregaUsuarios();
            CarregaTipoEvento();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CriarEvento(Evento evento)
        {
            string dataEvento = evento.data;
            string tipo = evento.tipo;
            bool dataResult = false;

            //retorna o id do usuário
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? idusuario = int.Parse(user);

            //instancia da classe ConsultasEvento
            ConsultasEvento consulta = new ();

            if(tipo == "Exclusivo")
            {
                //solicita e aguarda o retorno da consulta pra saber se já existe um evento exclusivo com a mesma data
                dataResult = await consulta.CriarEventoExclusivo(dataEvento);
            }

            if(dataResult == false)
            {
                if (ModelState.IsValid)
                {
                    evento.exclusivo = (int)idusuario;

                    //salva valores no banco
                    _contexto.Add(evento);
                    await _contexto.SaveChangesAsync();

                    Conexao conexao = new();
                    SqlCommand cmd = new();

                    //comando de consulto no banco e retornar a soma de todas as linhas
                    cmd.CommandText = "select max(eventoId) from Evento";

                    //conectar com o banco
                    cmd.Connection = conexao.Conectar();

                    // ler os dados retornados do banco
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (await reader.ReadAsync())
                    {
                        int idevento = reader.GetInt32(0);

                        //desconecta o banco
                        conexao.Desconectar();

                        try
                        {
                            //comando de pesquisa no banco
                            cmd.CommandText = "insert into usuariosEventos (idusuario, idevento) values (@idusuario, @idevento)";
                            //adicionar parametros
                            cmd.Parameters.AddWithValue("@idusuario", idusuario);
                            cmd.Parameters.AddWithValue("@idevento", idevento);


                            //conectar com o banco
                            cmd.Connection = conexao.Conectar();
                            // executa o comando
                            cmd.ExecuteNonQuery();
                            //desconecta com o banco
                            conexao.Desconectar();

                            //fechar o reader
                            reader.Close();

                        }
                        catch (SqlException e)
                        {

                        }

                    }
                    return RedirectToAction("Index", "Evento");
                }
                else
                {
                    CarregaTipoEvento();
                    return View(evento);
                }
            }
            else
            {
                CarregaTipoEvento();
                return View(evento);

            }

        }

        [HttpGet]
        public IActionResult AtualizarEvento(int? id)
        {
            if (id != null)
            {
                Evento evento = _contexto.Evento.Find(id);
                return View(evento);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarEvento(int? id, Evento evento)
        {
            if (id != null)
            {
                if (ModelState.IsValid)
                {
                    _contexto.Update(evento);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction("Index", "Evento", new { id = id });
                }
                else
                {
                    return View(evento);
                }
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult ExcluirEvento(int? id)
        {
            if (id != null)
            {
                Evento evento = _contexto.Evento.Find(id);
                return View(evento);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExcluirEvento(int? id, Evento evento)
        {
            if (id != null)
            {
                _contexto.Remove(evento);
                await _contexto.SaveChangesAsync();
                return RedirectToAction("Index", "Evento");
            }
            else
            {
                return NotFound();
            }
        }

        public void CarregaTipoEvento()
        {
            var ItemEvento = new List<SelectListItem>
            {
                new SelectListItem{Value = "Exclusivo", Text = "Exclusivo"},
                new SelectListItem{Value = "Compartilhado", Text = "Compartilhado"}
            };

            ViewBag.TipoEvento = ItemEvento;
        }

        public async void CarregaUsuarios()
        {
            ConsultasEvento usuarios = new();

            List<Usuario> listaUsuarios = await usuarios.BuscarUsuarios();

            ViewBag.Usuarios = new SelectList( listaUsuarios, "usuarioId", "nome");

        }
    }
}
