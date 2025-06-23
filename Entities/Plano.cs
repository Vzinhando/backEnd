using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[Table("Plano")]
public partial class Plano
{
    [Key]
    [Column("idPlano")]
    public int IdPlano { get; set; }

    [Column("tipoPlano")]
    [StringLength(50)]
    [Unicode(false)]
    public string TipoPlano { get; set; } = null!;

    [Column("valorPlano", TypeName = "decimal(10, 2)")]
    public decimal ValorPlano { get; set; }

    [Column("statusPlano")]
    [StringLength(30)]
    [Unicode(false)]
    public string StatusPlano { get; set; } = null!;

    [InverseProperty("IdPlanoNavigation")]
    public virtual ICollection<AssinaturaPlano> AssinaturaPlanos { get; set; } = new List<AssinaturaPlano>();
}
