using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class PerfilUsuario
{
    public int IdPerfil { get; set; }

    public int IdUsuario { get; set; }

    public string NomePerfil { get; set; } = null!;

    public string? CategoriaPerfil { get; set; }

    public string? DescricaoPerfil { get; set; }

    public string? FotoPerfilUrl { get; set; }

    public string? RedeSocialPerfil { get; set; }

    public string? TipoLocalUsuario { get; set; }

    public virtual ICollection<Contratacaoservico> Contratacaoservicos { get; set; } = new List<Contratacaoservico>();

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Servicooferecido> Servicooferecidos { get; set; } = new List<Servicooferecido>();

    public virtual ICollection<UsuarioAvaliaPerfil> UsuarioAvaliaPerfils { get; set; } = new List<UsuarioAvaliaPerfil>();
}
