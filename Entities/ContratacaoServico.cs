using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[Table("ContratacaoServico")]
public partial class ContratacaoServico
{
    [Key]
    [Column("idContratacaoServico")]
    public int IdContratacaoServico { get; set; }

    [Column("idUsuarioContratante")]
    public int IdUsuarioContratante { get; set; }

    [Column("idPerfilContratado")]
    public int IdPerfilContratado { get; set; }

    [Column("dataContratacao")]
    public DateOnly DataContratacao { get; set; }

    [Column("valorCombinado", TypeName = "decimal(10, 2)")]
    public decimal? ValorCombinado { get; set; }

    [Column("statusContratacao")]
    [StringLength(30)]
    [Unicode(false)]
    public string? StatusContratacao { get; set; }

    [ForeignKey("IdPerfilContratado")]
    [InverseProperty("ContratacaoServicos")]
    public virtual PerfilUsuario IdPerfilContratadoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuarioContratante")]
    [InverseProperty("ContratacaoServicos")]
    public virtual Usuario IdUsuarioContratanteNavigation { get; set; } = null!;

    [InverseProperty("IdContratacaoServicoNavigation")]
    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
