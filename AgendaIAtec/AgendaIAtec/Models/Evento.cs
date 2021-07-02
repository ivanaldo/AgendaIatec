using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaIAtec.Models
{
    public class Evento
    {
        public int eventoId { get; set; }

        [Required(ErrorMessage = "Informe o seu nome!", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        [MinLength(3, ErrorMessage = "Nome muito pequeno")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe seu e-mail!")]
        [StringLength(35, ErrorMessage = "Use menos caracteres!")]
        [EmailAddress(ErrorMessage = "Informe um email válido!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a sua senha!", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string senha { get; set; }

        [Required(ErrorMessage = "Informe sua data de Nascimento!")]
        public string dataNasc { get; set; }

        [Required(ErrorMessage = "Informe seu sexo")]
        public string sexo { get; set; }

        [Required(ErrorMessage = "Informe a categoria que pertence o evento")]
        public string categoria { get; set; }
    }
}
