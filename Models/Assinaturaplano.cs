using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class Assinaturaplano
{
    public int IdAssinaturaPlano { get; set; }

    public int IdUsuario { get; set; }

    public int IdPlano { get; set; }

    public DateOnly DataAssinatura { get; set; }

    public DateOnly? DataExpiracao { get; set; }

    public string StatusAssinatura { get; set; } = null!;

    public virtual Plano IdPlanoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
