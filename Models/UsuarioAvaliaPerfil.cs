using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class UsuarioAvaliaPerfil
{
    public int IdUsuarioAvaliador { get; set; }

    public int IdPerfilAvaliado { get; set; }

    public int IdAvaliacao { get; set; }

    public virtual Avaliacao IdAvaliacaoNavigation { get; set; } = null!;

    public virtual PerfilUsuario IdPerfilAvaliadoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioAvaliadorNavigation { get; set; } = null!;
}
