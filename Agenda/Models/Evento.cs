using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class Evento
    {
        
        public int eventoId { get; set; }

        [Display(Name = "Evento")]
        public string tipo { get; set; }

        [Required(ErrorMessage = "Informe o nome do evento!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        [MinLength(3, ErrorMessage = "Nome muito pequeno")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe uma descrição do evento!")]
        [StringLength(200, ErrorMessage = "Use menos caracteres!")]
        [Display(Name = "Descrição")]
        public string descricao { get; set; }

        [Required(ErrorMessage = "Informe a data do evento!")]
        [StringLength(10, ErrorMessage = "Caracteres excedido!")]
        [DataType(DataType.Date)]
        [Display(Name = "Data")]
        public string data { get; set; }

        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        [Required(ErrorMessage = "Informe o local do evento")]
        public string local { get; set; }

        [Required(ErrorMessage = "Informe os participantes do evento")]
        public string participantes { get; set; }

        public int exclusivo { get; set; }
    }
}
