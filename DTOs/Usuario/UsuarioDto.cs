namespace BackEndDemoday.DTOs.Usuario
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string TipoUsuario { get; set; }
        public DateOnly? DataCadastroUsuario { get; set; }
        public DateOnly? DataNascimentoUsuario { get; set; }
        public string? SexoUsuario { get; set; }
        public string? CepUsuario { get; set; }
        public string? EnderecoUsuario { get; set; }
        public string? CidadeUsuario { get; set; }
        public string? TelefoneUsuario { get; set; }
    }
}
