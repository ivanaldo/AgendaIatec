using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda.Models
{
    public class Usuario
    {
        public int usuarioId { get; set; }

        [Required(ErrorMessage = "Informe seu nome!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        [MinLength(3, ErrorMessage = "Nome muito curto")]
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [EmailAddress(ErrorMessage = "Informe um e-mail válido!")]
        [StringLength(45, ErrorMessage = "Use menos caracteres")]
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe seu e-mail!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a sua senha!")]
        [MinLength(6, ErrorMessage = "Senha precisa ter no mínimo 6 caracteres")]
        [StringLength(15, ErrorMessage = "Valor excedido!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string senha { get; set; }

        [Required(ErrorMessage = "Informe sua data de Nascimento!")]
        [StringLength(10, ErrorMessage = "Caracteres excedido!")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de nascimento")]
        public string data { get; set; }


        [Required(ErrorMessage = "Informe seu sexo")]
        [Display(Name = "Seu sexo")]
        public string sexo { get; set; }
    }
}
