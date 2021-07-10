using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class Conexao
    {
        SqlConnection conexao = new SqlConnection();

        public Conexao()
        {
            conexao.ConnectionString = "Data Source=CARVALHO\\SQLEXPRESS;Initial Catalog=AgendaIatec;Integrated Security=True;";
        }

        public SqlConnection Conectar()
        {
            if (conexao.State == System.Data.ConnectionState.Closed)
            {
                 conexao.Open();
            }

            return conexao;
        }

        public void Desconectar()
        {
            if (conexao.State == System.Data.ConnectionState.Open)
            {
                conexao.Close();
            }
        }
    }
}
