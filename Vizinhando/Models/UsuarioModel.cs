using System.ComponentModel.DataAnnotations;

namespace Vizinhando.Models
{
    public class UsuarioModel
    {

        [Key]
        public int IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string SenhaUsuario { get; set; }
        public string cepUsuario { get; set; }

    }
}
