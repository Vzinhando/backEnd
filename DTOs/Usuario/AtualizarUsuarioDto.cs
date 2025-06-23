using System.ComponentModel.DataAnnotations;

public class AtualizarUsuarioDto
{
    [Required]
    [StringLength(150)]
    public string NomeUsuario { get; set; }

    public DateOnly? DataNascimentoUsuario { get; set; }
    public string? SexoUsuario { get; set; }
    public string? CepUsuario { get; set; }
    public string? EnderecoUsuario { get; set; }
    public string? CidadeUsuario { get; set; }
    public string? TelefoneUsuario { get; set; }
}