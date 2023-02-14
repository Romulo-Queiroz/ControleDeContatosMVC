using System.ComponentModel.DataAnnotations;

namespace ControleDeContatosMVC.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Digite o nome do contato")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do contato")]
        [EmailAddress(ErrorMessage ="o e-mail informado não é válido!")]
        public string Email { get; set;}

        [Required(ErrorMessage = "Digite o celular do contato")]
        [Phone(ErrorMessage ="o celular informado não é válido!")]
        public string Celular { get; set;}

        public int? usuarioId { get; set; }

        public UsuarioModel Usuario { get; set; }
    }
}
