using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class Cupomdesconto
{
    public int IdCupom { get; set; }

    public string CodigoCupom { get; set; } = null!;

    public string? TipoCupom { get; set; }

    public decimal? PorcentagemDescontoCupom { get; set; }

    public string StatusCupom { get; set; } = null!;

    public DateOnly? DataValidadeCupom { get; set; }

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
