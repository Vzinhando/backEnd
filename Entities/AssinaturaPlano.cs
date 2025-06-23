using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[Table("AssinaturaPlano")]
public partial class AssinaturaPlano
{
    [Key]
    [Column("idAssinaturaPlano")]
    public int IdAssinaturaPlano { get; set; }

    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column("idPlano")]
    public int IdPlano { get; set; }

    [Column("dataAssinatura")]
    public DateOnly DataAssinatura { get; set; }

    [Column("dataExpiracao")]
    public DateOnly? DataExpiracao { get; set; }

    [Column("statusAssinatura")]
    [StringLength(30)]
    [Unicode(false)]
    public string StatusAssinatura { get; set; } = null!;

    [ForeignKey("IdPlano")]
    [InverseProperty("AssinaturaPlanos")]
    public virtual Plano IdPlanoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuario")]
    [InverseProperty("AssinaturaPlanos")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdAssinaturaPlanoNavigation")]
    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
