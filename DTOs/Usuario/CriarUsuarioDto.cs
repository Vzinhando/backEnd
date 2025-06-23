using System.ComponentModel.DataAnnotations;

namespace BackEndDemoday.DTOs.Usuario
{
    public class CriarUsuarioDto
    {
        [Required]
        [StringLength(150)]
        public string NomeUsuario { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string EmailUsuario { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]  
        public string Senha { get; set; }

        [Required]
        public string TipoUsuario { get; set; } 

        public DateOnly? DataNascimentoUsuario { get; set; }
        public string? SexoUsuario { get; set; }
        public string? CepUsuario { get; set; }
        public string? EnderecoUsuario { get; set; }
        public string? CidadeUsuario { get; set; }
        public string? TelefoneUsuario { get; set; }
    }
}
