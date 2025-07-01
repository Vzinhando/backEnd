namespace ApiDemoday.DTOs.Usuario;

public class UsuarioExibicaoDto
{
    public int IdUsuario { get; set; }
    public string NomeUsuario { get; set; } = null!;
    public string EmailUsuario { get; set; } = null!;
    public string TipoUsuario { get; set; } = null!;
    public DateOnly? DataNascimentoUsuario { get; set; }
    public string? TelefoneUsuario { get; set; }
    public string? CepUsuario { get; set; }
    public string? EnderecoUsuario { get; set; }
    public string? CidadeUsuario { get; set; }
}