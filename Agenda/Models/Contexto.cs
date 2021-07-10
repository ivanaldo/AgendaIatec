using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> UsuarioCadastro { get; set; }

        public DbSet<Evento> Evento { get; set; }

        public DbSet<UsuariosEventos> UsuariosEventos { get; set; }

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
    }
}
