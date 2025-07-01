using System.ComponentModel.DataAnnotations;

namespace ApiDemoday.DTOs.Usuario;

public class UsuarioUpdateDto
{
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    public string? NomeUsuario { get; set; }

    public DateOnly? DataNascimentoUsuario { get; set; }

    [StringLength(15)]
    public string? TelefoneUsuario { get; set; }

    [StringLength(8)]
    public string? CepUsuario { get; set; }

    public string? EnderecoUsuario { get; set; }

    public string? CidadeUsuario { get; set; }
}
