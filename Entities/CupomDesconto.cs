using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[Table("CupomDesconto")]
[Index("CodigoCupom", Name = "UQ_Cupom_Codigo", IsUnique = true)]
public partial class CupomDesconto
{
    [Key]
    [Column("idCupom")]
    public int IdCupom { get; set; }

    [Column("codigoCupom")]
    [StringLength(50)]
    [Unicode(false)]
    public string CodigoCupom { get; set; } = null!;

    [Column("tipoCupom")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoCupom { get; set; }

    [Column("porcentagemDescontoCupom", TypeName = "decimal(5, 2)")]
    public decimal? PorcentagemDescontoCupom { get; set; }

    [Column("statusCupom")]
    [StringLength(30)]
    [Unicode(false)]
    public string StatusCupom { get; set; } = null!;

    [Column("dataValidadeCupom")]
    public DateOnly? DataValidadeCupom { get; set; }

    [InverseProperty("IdCupomNavigation")]
    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();
}
