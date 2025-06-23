using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[Table("ServicoOferecido")]
public partial class ServicoOferecido
{
    [Key]
    [Column("idServicoOferecido")]
    public int IdServicoOferecido { get; set; }

    [Column("idPerfil")]
    public int IdPerfil { get; set; }

    [Column("nomeServico")]
    [StringLength(100)]
    [Unicode(false)]
    public string NomeServico { get; set; } = null!;

    [Column("descricaoServico")]
    [StringLength(500)]
    [Unicode(false)]
    public string? DescricaoServico { get; set; }

    [Column("precoBase", TypeName = "decimal(10, 2)")]
    public decimal? PrecoBase { get; set; }

    [ForeignKey("IdPerfil")]
    [InverseProperty("ServicoOferecidos")]
    public virtual PerfilUsuario IdPerfilNavigation { get; set; } = null!;
}
