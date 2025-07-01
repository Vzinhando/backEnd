using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class Avaliacao
{
    public int IdAvaliacao { get; set; }

    public string? DescricaoAvaliacao { get; set; }

    public decimal EstrelasAvaliacao { get; set; }

    public DateOnly? DataAvaliacao { get; set; }

    public virtual ICollection<UsuarioAvaliaPerfil> UsuarioAvaliaPerfils { get; set; } = new List<UsuarioAvaliaPerfil>();
}
