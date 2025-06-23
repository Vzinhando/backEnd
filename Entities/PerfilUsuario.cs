using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[Table("Perfil_Usuario")]
public partial class PerfilUsuario
{
    [Key]
    [Column("idPerfil")]
    public int IdPerfil { get; set; }

    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column("nomePerfil")]
    [StringLength(100)]
    [Unicode(false)]
    public string NomePerfil { get; set; } = null!;

    [Column("categoriaPerfil")]
    [StringLength(50)]
    [Unicode(false)]
    public string? CategoriaPerfil { get; set; }

    [Column("descricaoPerfil")]
    [StringLength(500)]
    [Unicode(false)]
    public string? DescricaoPerfil { get; set; }

    [Column("fotoPerfilUrl")]
    [Unicode(false)]
    public string? FotoPerfilUrl { get; set; }

    [Column("redeSocialPerfil")]
    [StringLength(255)]
    [Unicode(false)]
    public string? RedeSocialPerfil { get; set; }

    [Column("tipoLocalUsuario")]
    [StringLength(50)]
    [Unicode(false)]
    public string? TipoLocalUsuario { get; set; }

    [InverseProperty("IdPerfilContratadoNavigation")]
    public virtual ICollection<ContratacaoServico> ContratacaoServicos { get; set; } = new List<ContratacaoServico>();

    [ForeignKey("IdUsuario")]
    [InverseProperty("PerfilUsuarios")]
    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    [InverseProperty("IdPerfilNavigation")]
    public virtual ICollection<ServicoOferecido> ServicoOferecidos { get; set; } = new List<ServicoOferecido>();

    [InverseProperty("IdPerfilAvaliadoNavigation")]
    public virtual ICollection<UsuarioAvaliaPerfil> UsuarioAvaliaPerfils { get; set; } = new List<UsuarioAvaliaPerfil>();
}
