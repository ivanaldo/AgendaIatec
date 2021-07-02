using System.ComponentModel.DataAnnotations;

namespace AgendaIAtec.Models
{
    public class Usuario
    {
        public int usuarioId { get; set; }

        [Required(ErrorMessage = "Informe o seu nome!", AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        [MinLength(3, ErrorMessage = "Nome muito pequeno")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Informe seu e-mail!")]
        [StringLength(35, ErrorMessage = "Use menos caracteres!")]
        [EmailAddress(ErrorMessage = "Informe um email válido!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a sua senha!", AllowEmptyStrings = false)]
        [MinLength(6, ErrorMessage = "Senha precisa ter no mínimo 6 caracteres")]
        [StringLength(15, ErrorMessage = "Valor excedido!")]
        [DataType(DataType.Password)]
        public string senha { get; set; }

        [Required(ErrorMessage = "Informe sua data de Nascimento!")]
        [StringLength(10, ErrorMessage = "Caracteres excedido!")]
        [DataType(DataType.Date)]
        public string dataNasc { get; set; }

        [Required(ErrorMessage = "Informe seu sexo")]
        [StringLength(9, MinimumLength = 8, ErrorMessage = "Valor inválido!")]
        public string sexo { get; set; }
    }
}
