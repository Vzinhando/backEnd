using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BackEndDemoday.Entities;

[Table("Usuario")]
[Index("EmailUsuario", Name = "UQ_Usuario_Email", IsUnique = true)]
public partial class Usuario
{
    [Key]
    [Column("idUsuario")]
    public int IdUsuario { get; set; }

    [Column("nomeUsuario")]
    [StringLength(150)]
    [Unicode(false)]
    public string NomeUsuario { get; set; } = null!;

    [Column("emailUsuario")]
    [StringLength(255)]
    [Unicode(false)]
    public string EmailUsuario { get; set; } = null!;

    [Column("senhaUsuario")]
    [StringLength(255)]
    [Unicode(false)]
    public string SenhaUsuario { get; set; } = null!;

    [Column("tipoUsuario")]
    [StringLength(30)]
    [Unicode(false)]
    public string TipoUsuario { get; set; } = null!;

    [Column("dataCadastroUsuario")]
    public DateOnly? DataCadastroUsuario { get; set; }

    [Column("dataNascimentoUsuario")]
    public DateOnly? DataNascimentoUsuario { get; set; }

    [Column("sexoUsuario")]
    [StringLength(1)]
    [Unicode(false)]
    public string? SexoUsuario { get; set; }

    [Column("cepUsuario")]
    [StringLength(9)]
    [Unicode(false)]
    public string? CepUsuario { get; set; }

    [Column("enderecoUsuario")]
    [StringLength(255)]
    [Unicode(false)]
    public string? EnderecoUsuario { get; set; }

    [Column("cidadeUsuario")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CidadeUsuario { get; set; }

    [Column("telefoneUsuario")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TelefoneUsuario { get; set; }

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<AssinaturaPlano> AssinaturaPlanos { get; set; } = new List<AssinaturaPlano>();

    [InverseProperty("IdUsuarioContratanteNavigation")]
    public virtual ICollection<ContratacaoServico> ContratacaoServicos { get; set; } = new List<ContratacaoServico>();

    [InverseProperty("IdUsuarioNavigation")]
    public virtual ICollection<PerfilUsuario> PerfilUsuarios { get; set; } = new List<PerfilUsuario>();

    [InverseProperty("IdUsuarioAvaliadorNavigation")]
    public virtual ICollection<UsuarioAvaliaPerfil> UsuarioAvaliaPerfils { get; set; } = new List<UsuarioAvaliaPerfil>();
}
