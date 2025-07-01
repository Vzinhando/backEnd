using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class Servicooferecido
{
    public int IdServicoOferecido { get; set; }

    public int IdPerfil { get; set; }

    public string NomeServico { get; set; } = null!;

    public string? DescricaoServico { get; set; }

    public decimal? PrecoBase { get; set; }

    public virtual PerfilUsuario IdPerfilNavigation { get; set; } = null!;
}
