using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[Table("Pagamento")]
public partial class Pagamento
{
    [Key]
    [Column("idPagamento")]
    public int IdPagamento { get; set; }

    [Column("idContratacaoServico")]
    public int? IdContratacaoServico { get; set; }

    [Column("idAssinaturaPlano")]
    public int? IdAssinaturaPlano { get; set; }

    [Column("idCupom")]
    public int? IdCupom { get; set; }

    [Column("valorBruto", TypeName = "decimal(10, 2)")]
    public decimal ValorBruto { get; set; }

    [Column("valorDesconto", TypeName = "decimal(10, 2)")]
    public decimal? ValorDesconto { get; set; }

    [Column("valorFinal", TypeName = "decimal(10, 2)")]
    public decimal ValorFinal { get; set; }

    [Column("formaPagamento")]
    [StringLength(50)]
    [Unicode(false)]
    public string? FormaPagamento { get; set; }

    [Column("idTransacao")]
    [StringLength(255)]
    [Unicode(false)]
    public string? IdTransacao { get; set; }

    [Column("statusPagamento")]
    [StringLength(30)]
    [Unicode(false)]
    public string StatusPagamento { get; set; } = null!;

    [Column("dataPagamento", TypeName = "datetime")]
    public DateTime? DataPagamento { get; set; }

    [ForeignKey("IdAssinaturaPlano")]
    [InverseProperty("Pagamentos")]
    public virtual AssinaturaPlano? IdAssinaturaPlanoNavigation { get; set; }

    [ForeignKey("IdContratacaoServico")]
    [InverseProperty("Pagamentos")]
    public virtual ContratacaoServico? IdContratacaoServicoNavigation { get; set; }

    [ForeignKey("IdCupom")]
    [InverseProperty("Pagamentos")]
    public virtual CupomDesconto? IdCupomNavigation { get; set; }
}
