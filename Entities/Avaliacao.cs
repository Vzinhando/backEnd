using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[Table("Avaliacao")]
public partial class Avaliacao
{
    [Key]
    [Column("idAvaliacao")]
    public int IdAvaliacao { get; set; }

    [Column("descricaoAvaliacao")]
    [StringLength(500)]
    [Unicode(false)]
    public string? DescricaoAvaliacao { get; set; }

    [Column("estrelasAvaliacao", TypeName = "decimal(3, 1)")]
    public decimal EstrelasAvaliacao { get; set; }

    [Column("dataAvaliacao")]
    public DateOnly? DataAvaliacao { get; set; }

    [InverseProperty("IdAvaliacaoNavigation")]
    public virtual ICollection<UsuarioAvaliaPerfil> UsuarioAvaliaPerfils { get; set; } = new List<UsuarioAvaliaPerfil>();
}
