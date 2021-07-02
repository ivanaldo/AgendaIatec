using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaIAtec.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Cadastro { get; set; }

        public DbSet<Evento> Evento { get; set; }

        

        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {

        }
    }
}
