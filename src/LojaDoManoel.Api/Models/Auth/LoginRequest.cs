using System.ComponentModel.DataAnnotations;

namespace LojaDoManoel.Api.Models.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username é obrigatório")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres")]
        public string Password { get; set; }
    }
}