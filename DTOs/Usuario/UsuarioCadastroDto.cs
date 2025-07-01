using System.ComponentModel.DataAnnotations;

namespace ApiDemoday.DTOs.Usuario;

public class UsuarioCadastroDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string? NomeUsuario { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
    public string? EmailUsuario { get; set; } 

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 20 caracteres.")]
    public string? SenhaUsuario { get; set; } 

    [Required(ErrorMessage = "O CEP é obrigatório.")]
    public string? CepUsuario { get; set; } 
}