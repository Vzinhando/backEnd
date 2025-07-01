using System;
using System.Collections.Generic;

namespace ApiDemoday.Models;

public partial class Pagamento
{
    public int IdPagamento { get; set; }

    public int? IdContratacaoServico { get; set; }

    public int? IdAssinaturaPlano { get; set; }

    public int? IdCupom { get; set; }

    public decimal ValorBruto { get; set; }

    public decimal? ValorDesconto { get; set; }

    public decimal ValorFinal { get; set; }

    public string? FormaPagamento { get; set; }

    public string? IdTransacao { get; set; }

    public string StatusPagamento { get; set; } = null!;

    public DateTime? DataPagamento { get; set; }

    public virtual Assinaturaplano? IdAssinaturaPlanoNavigation { get; set; }

    public virtual Contratacaoservico? IdContratacaoServicoNavigation { get; set; }

    public virtual Cupomdesconto? IdCupomNavigation { get; set; }
}
