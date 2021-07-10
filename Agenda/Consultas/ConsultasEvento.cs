using Agenda.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agenda.Consultas
{
    public class ConsultasEvento
    {

        public async Task<List<Evento>> MeusEventos(int? idusuario)
        {
            List<Evento> list = new();

            Conexao conexao = new();
            SqlCommand comando = new();

            comando.CommandText = $@"select * from Evento where Evento.exclusivo = '{idusuario}' order by Evento.data";

            //conectar com o banco
            comando.Connection = conexao.Conectar();

            // ler os dados retornados do banco
            using (SqlDataReader reader = comando.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                    Evento eventoResultado = new();

                    eventoResultado.eventoId = (int)reader["eventoId"];
                    eventoResultado.tipo = reader["tipo"].ToString();
                    eventoResultado.nome = reader["nome"].ToString();
                    eventoResultado.participantes = reader["participantes"].ToString();
                    eventoResultado.local = reader["local"].ToString();
                    eventoResultado.data = reader["data"].ToString();
                    eventoResultado.descricao = reader["descricao"].ToString();

                        list.Add(eventoResultado);
                }

                //desconecta o banco
                conexao.Desconectar();
                //fechar o reader
                reader.Close();

            }
            return list;
        }

        public async Task<List<Evento>> EventosAndamento(int? idusuario)
        {
            List<Evento> list = new ();

            Conexao conexao = new();
            SqlCommand comando = new();

            comando.CommandText = $@"select * from Evento  inner join UsuariosEventos on
            UsuariosEventos.idevento = Evento.eventoId inner join UsuarioCadastro on UsuariosEventos.idusuario =
            UsuarioCadastro.usuarioId where UsuarioCadastro.usuarioId = '{idusuario}' order by Evento.data";

            //conectar com o banco
            comando.Connection = conexao.Conectar();

            // ler os dados retornados do banco
            using (SqlDataReader reader = comando.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                    DateTime dataA = DateTime.Today;
                    Evento eventoResultado = new ();

                    eventoResultado.eventoId = (int)reader["eventoId"];
                    eventoResultado.tipo = reader["tipo"].ToString();
                    eventoResultado.nome = reader["nome"].ToString();
                    eventoResultado.participantes = reader["participantes"].ToString();
                    eventoResultado.local = reader["local"].ToString();
                    eventoResultado.data = reader["data"].ToString();
                    eventoResultado.descricao = reader["descricao"].ToString();

                    //atribui a data a data do dia já formatada para o padrão brasileiro
                    string dataAtual = dataA.ToString("dd/MM/yyyy");
                    //formata a data do evento para o padrão brasileiro
                    string dataEvento = DateTime.Parse(eventoResultado.data.ToString()).ToString("dd/MM/yyyy");
                    //compara as datas
                    if (dataEvento.Equals(dataAtual))
                    {
                        list.Add(eventoResultado);
                    }


                }

                //desconecta o banco
                conexao.Desconectar();
                //fechar o reader
                reader.Close();

            }
            return list;
        }

        public async Task<List<Evento>> ProximoEvento(int? idusuario)
        {
            List<Evento> list = new ();

            Conexao conexao = new();
            SqlCommand comando = new();

            comando.CommandText = $@"select * from Evento  inner join UsuariosEventos on
            UsuariosEventos.idevento = Evento.eventoId inner join UsuarioCadastro on UsuariosEventos.idusuario =
            UsuarioCadastro.usuarioId where UsuarioCadastro.usuarioId = '{idusuario}' order by Evento.data";

            //conectar com o banco
            comando.Connection = conexao.Conectar();

            // ler os dados retornados do banco
            using (SqlDataReader reader = comando.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                    DateTime dataHoje = DateTime.Today;
                    Evento eventoResultado = new ();

                    eventoResultado.eventoId = (int)reader["eventoId"];
                    eventoResultado.tipo = reader["tipo"].ToString();
                    eventoResultado.nome = reader["nome"].ToString();
                    eventoResultado.participantes = reader["participantes"].ToString();
                    eventoResultado.local = reader["local"].ToString();
                    eventoResultado.data = reader["data"].ToString();
                    eventoResultado.descricao = reader["descricao"].ToString();

                    string dataAtual = dataHoje.ToString("dd.MM.yyyy");
                    string dataEvento = DateTime.Parse(eventoResultado.data.ToString()).ToString("dd.MM.yyyy");

                    DateTime dataEvenDate = Convert.ToDateTime(dataEvento);
                    DateTime dataAtuaDate = Convert.ToDateTime(dataAtual);

                    if (dataEvento != dataAtual && dataEvenDate > dataAtuaDate)
                    {
                        list.Add(eventoResultado);
                    }

                }

                //desconecta o banco
                conexao.Desconectar();
                //fechar o reader
                reader.Close();

            }
            return list;
        }

        public async Task<List<Evento>> Exclusivo(int? idusuario)
        {
            List<Evento> list = new ();

            Conexao conexao = new();
            SqlCommand comando = new();

            comando.CommandText = $@"select * from Evento  inner join UsuariosEventos on
            UsuariosEventos.idevento = Evento.eventoId inner join UsuarioCadastro on UsuariosEventos.idusuario =
            UsuarioCadastro.usuarioId where UsuarioCadastro.usuarioId = '{idusuario}' and Evento.tipo like '%Exclusivo%' order by Evento.data";

            //conectar com o banco
            comando.Connection = conexao.Conectar();

            // ler os dados retornados do banco
            using (SqlDataReader reader = comando.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                    //instacia um objeto
                    Evento eventoResultado = new ();

                    eventoResultado.eventoId = (int)reader["eventoId"];
                    eventoResultado.tipo = reader["tipo"].ToString();
                    eventoResultado.nome = reader["nome"].ToString();
                    eventoResultado.participantes = reader["participantes"].ToString();
                    eventoResultado.local = reader["local"].ToString();
                    eventoResultado.data = reader["data"].ToString();
                    eventoResultado.descricao = reader["descricao"].ToString();

                        list.Add(eventoResultado);

                }

                //desconecta o banco
                conexao.Desconectar();
                //fechar o reader
                reader.Close();

            }

            return list;
        }

        public async Task<List<Evento>> Compartilhado(int? idusuario)
        {
            List<Evento> list = new ();

            Conexao conexao = new();
            SqlCommand comando = new();

            comando.CommandText = $@"select * from Evento  inner join UsuariosEventos on
            UsuariosEventos.idevento = Evento.eventoId inner join UsuarioCadastro on UsuariosEventos.idusuario =
            UsuarioCadastro.usuarioId where UsuarioCadastro.usuarioId = '{idusuario}' and Evento.tipo like '%Compartilhado%' order by Evento.data";

            //conectar com o banco
            comando.Connection = conexao.Conectar();

            // ler os dados retornados do banco
            using (SqlDataReader reader = comando.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                    //instacia um objeto
                    Evento eventoResultado = new ();

                    eventoResultado.eventoId = (int)reader["eventoId"];
                    eventoResultado.tipo = reader["tipo"].ToString();
                    eventoResultado.nome = reader["nome"].ToString();
                    eventoResultado.participantes = reader["participantes"].ToString();
                    eventoResultado.local = reader["local"].ToString();
                    eventoResultado.data = reader["data"].ToString();
                    eventoResultado.descricao = reader["descricao"].ToString();

                        list.Add(eventoResultado);

                }

                //desconecta o banco
                conexao.Desconectar();
                //fechar o reader
                reader.Close();

            }

            return list;
        }

        public async Task<bool> CriarEventoExclusivo(string dataEvento)
        {
            string data;
            bool retornoData = false;

            Conexao conexao = new();
            SqlCommand cmd = new();

            //consulta no banco e retornar as data de todos eventos exclusivo
            cmd.CommandText = $"select  data from Evento where Evento.tipo like '%Exclusivo%'";

            //conectar com o banco
            cmd.Connection = conexao.Conectar();

            // ler os dados retornados do banco
            SqlDataReader reader = cmd.ExecuteReader();

            while(await reader.ReadAsync())
            {
                //instancia um objeto
                Evento pesquisaResultado = new();
                //ler o retorno do banco e depois atribui valor data para o objeto
                pesquisaResultado.data = reader["data"].ToString();
                data = pesquisaResultado.data;

                if(data == dataEvento)
                {
                    retornoData = true;
                    //fecha o reader
                    reader.Close();
                    break;
                }
                else
                {
                    retornoData = false;
                }
               
            }
            //desconecta o banco
            conexao.Desconectar();
            //fecha o reader
            reader.Close();
            return retornoData;
        }

        public async Task<List<Usuario>> BuscarUsuarios()
        {
            List<Usuario> list = new();

            Conexao conexao = new();
            SqlCommand comando = new();

            comando.CommandText = $@"select usuarioId, nome from UsuarioCadastro";

            //conectar com o banco
            comando.Connection = conexao.Conectar();

            // ler os dados retornados do banco
            using (SqlDataReader reader = comando.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                    //instacia um objeto
                    Usuario usuarioResultado = new();

                    usuarioResultado.usuarioId = (int)reader["usuarioId"];
                    usuarioResultado.nome = reader["nome"].ToString();

                    list.Add(usuarioResultado);

                }

                //desconecta o banco
                conexao.Desconectar();
                //fechar o reader
                reader.Close();

            }

            return list;
        }

        public async Task<List<Evento>> BuscaPorData(int? idusuario, string data)
        {
            List<Evento> list = new();

            Conexao conexao = new();
            SqlCommand comando = new();

            comando.CommandText = $@"select * from Evento  inner join UsuariosEventos on
            UsuariosEventos.idevento = Evento.eventoId inner join UsuarioCadastro on UsuariosEventos.idusuario =
            UsuarioCadastro.usuarioId where UsuarioCadastro.usuarioId = '{idusuario}' and Evento.data like '{data}' order by Evento.data";

            //conectar com o banco
            comando.Connection = conexao.Conectar();

            // ler os dados retornados do banco
            using (SqlDataReader reader = comando.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {
                   
                    //instacia um objeto
                    Evento eventoResultado = new();

                    eventoResultado.eventoId = (int)reader["eventoId"];
                    eventoResultado.tipo = reader["tipo"].ToString();
                    eventoResultado.nome = reader["nome"].ToString();
                    eventoResultado.participantes = reader["participantes"].ToString();
                    eventoResultado.local = reader["local"].ToString();
                    eventoResultado.data = reader["data"].ToString();
                    eventoResultado.descricao = reader["descricao"].ToString();

                    list.Add(eventoResultado);

                }

                //desconecta o banco
                conexao.Desconectar();
                //fechar o reader
                reader.Close();

            }

            return list;
        }

        public async Task<List<Evento>> BuscaPorNome(int? idusuario, string nome)
        {
            List<Evento> list = new();

            Conexao conexao = new();
            SqlCommand comando = new();

            comando.CommandText = $@"select * from Evento  inner join UsuariosEventos on
            UsuariosEventos.idevento = Evento.eventoId inner join UsuarioCadastro on UsuariosEventos.idusuario =
            UsuarioCadastro.usuarioId where UsuarioCadastro.usuarioId = '{idusuario}' and Evento.nome like '{nome}' order by Evento.data";

            //conectar com o banco
            comando.Connection = conexao.Conectar();

            // ler os dados retornados do banco
            using (SqlDataReader reader = comando.ExecuteReader())
            {

                while (await reader.ReadAsync())
                {

                    //instacia um objeto
                    Evento eventoResultado = new();

                    eventoResultado.eventoId = (int)reader["eventoId"];
                    eventoResultado.tipo = reader["tipo"].ToString();
                    eventoResultado.nome = reader["nome"].ToString();
                    eventoResultado.participantes = reader["participantes"].ToString();
                    eventoResultado.local = reader["local"].ToString();
                    eventoResultado.data = reader["data"].ToString();
                    eventoResultado.descricao = reader["descricao"].ToString();

                    list.Add(eventoResultado);

                }

                //desconecta o banco
                conexao.Desconectar();
                //fechar o reader
                reader.Close();

            }

            return list;
        }
    }
}
