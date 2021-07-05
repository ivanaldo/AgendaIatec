using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace Agenda.Models
{
    [Keyless]
    public class UsuariosEventos
    {

        [ForeignKey("Usuario")]
        public int idusuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Evento")]
        public int idevento { get; set; }
        public virtual Evento Evento { get; set; }
    }
}
