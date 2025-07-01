using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class Contratacaoservico
{
    public int IdContratacaoServico { get; set; }

    public int IdUsuarioContratante { get; set; }

    public int IdPerfilContratado { get; set; }

    public DateOnly DataContratacao { get; set; }

    public decimal? ValorCombinado { get; set; }

    public string? StatusContratacao { get; set; }

    public virtual PerfilUsuario IdPerfilContratadoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioContratanteNavigation { get; set; } = null!;

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
