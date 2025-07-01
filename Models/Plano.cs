using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class Plano
{
    public int IdPlano { get; set; }

    public string TipoPlano { get; set; } = null!;

    public decimal ValorPlano { get; set; }

    public string StatusPlano { get; set; } = null!;

    public virtual ICollection<Assinaturaplano> Assinaturaplanos { get; set; } = new List<Assinaturaplano>();
}
