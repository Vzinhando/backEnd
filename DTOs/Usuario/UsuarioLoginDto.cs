using System.ComponentModel.DataAnnotations;

namespace ApiDemoday.DTOs.Usuario;

public class UsuarioLoginDto
{
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
    public string EmailUsuario { get; set; } = null!;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string SenhaUsuario { get; set; } = null!;
}