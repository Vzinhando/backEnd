using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NomeUsuario { get; set; } = null!;

    public string EmailUsuario { get; set; } = null!;

    public string SenhaUsuario { get; set; } = null!;

    public string TipoUsuario { get; set; } = null!;

    public DateOnly? DataCadastroUsuario { get; set; }

    public DateOnly? DataNascimentoUsuario { get; set; }

    public string? SexoUsuario { get; set; }

    public string? CepUsuario { get; set; }

    public string? EnderecoUsuario { get; set; }

    public string? CidadeUsuario { get; set; }

    public string? TelefoneUsuario { get; set; }
    public string? FotoUsuario { get; set; }

    public virtual ICollection<Assinaturaplano> Assinaturaplanos { get; set; } = new List<Assinaturaplano>();

    public virtual ICollection<Contratacaoservico> Contratacaoservicos { get; set; } = new List<Contratacaoservico>();

    public virtual ICollection<PerfilUsuario> PerfilUsuarios { get; set; } = new List<PerfilUsuario>();

    public virtual ICollection<UsuarioAvaliaPerfil> UsuarioAvaliaPerfils { get; set; } = new List<UsuarioAvaliaPerfil>();
}
