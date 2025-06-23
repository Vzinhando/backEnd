using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[PrimaryKey("IdUsuarioAvaliador", "IdPerfilAvaliado")]
[Table("Usuario_Avalia_Perfil")]
public partial class UsuarioAvaliaPerfil
{
    [Key]
    [Column("idUsuarioAvaliador")]
    public int IdUsuarioAvaliador { get; set; }

    [Key]
    [Column("idPerfilAvaliado")]
    public int IdPerfilAvaliado { get; set; }

    [Column("idAvaliacao")]
    public int IdAvaliacao { get; set; }

    [ForeignKey("IdAvaliacao")]
    [InverseProperty("UsuarioAvaliaPerfils")]
    public virtual Avaliacao IdAvaliacaoNavigation { get; set; } = null!;

    [ForeignKey("IdPerfilAvaliado")]
    [InverseProperty("UsuarioAvaliaPerfils")]
    public virtual PerfilUsuario IdPerfilAvaliadoNavigation { get; set; } = null!;

    [ForeignKey("IdUsuarioAvaliador")]
    [InverseProperty("UsuarioAvaliaPerfils")]
    public virtual Usuario IdUsuarioAvaliadorNavigation { get; set; } = null!;
}
