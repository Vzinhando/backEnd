namespace BackEndDemoday.Services;
using BackEndDemoday.DTOs.Usuario;
using BackEndDemoday.Entities;
using BCrypt.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioDto> CriarAsync(CriarUsuarioDto criarUsuarioDto)
    {
        if (await _usuarioRepository.ObterPorEmailAsync(criarUsuarioDto.EmailUsuario) != null)
        {
            return null;
        }

        var senhaHash = BCrypt.HashPassword(criarUsuarioDto.Senha);

        var usuario = new Usuario
        {
            NomeUsuario = criarUsuarioDto.NomeUsuario,
            EmailUsuario = criarUsuarioDto.EmailUsuario,
            SenhaUsuario = senhaHash,
            TipoUsuario = criarUsuarioDto.TipoUsuario,
            DataCadastroUsuario = DateOnly.FromDateTime(DateTime.UtcNow),
            DataNascimentoUsuario = criarUsuarioDto.DataNascimentoUsuario,
            SexoUsuario = criarUsuarioDto.SexoUsuario,
            CepUsuario = criarUsuarioDto.CepUsuario,
            EnderecoUsuario = criarUsuarioDto.EnderecoUsuario,
            CidadeUsuario = criarUsuarioDto.CidadeUsuario,
            TelefoneUsuario = criarUsuarioDto.TelefoneUsuario
        };

        await _usuarioRepository.AdicionarAsync(usuario);
        await _usuarioRepository.SalvarTudoAsync();

        return MapearParaDto(usuario);
    }

    public async Task<bool> AtualizarAsync(int id, AtualizarUsuarioDto atualizarUsuarioDto)
    {
        var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuarioExistente == null) return false;

        usuarioExistente.NomeUsuario = atualizarUsuarioDto.NomeUsuario;
        usuarioExistente.DataNascimentoUsuario = atualizarUsuarioDto.DataNascimentoUsuario;
        usuarioExistente.SexoUsuario = atualizarUsuarioDto.SexoUsuario;
        usuarioExistente.CepUsuario = atualizarUsuarioDto.CepUsuario;
        usuarioExistente.EnderecoUsuario = atualizarUsuarioDto.EnderecoUsuario;
        usuarioExistente.CidadeUsuario = atualizarUsuarioDto.CidadeUsuario;
        usuarioExistente.TelefoneUsuario = atualizarUsuarioDto.TelefoneUsuario;

        _usuarioRepository.Atualizar(usuarioExistente);
        return await _usuarioRepository.SalvarTudoAsync();
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var usuarioExistente = await _usuarioRepository.ObterPorIdAsync(id);
        if (usuarioExistente == null) return false;

        _usuarioRepository.Remover(usuarioExistente);
        return await _usuarioRepository.SalvarTudoAsync();
    }

    public async Task<UsuarioDto> ObterPorIdAsync(int id)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(id);
        return usuario == null ? null : MapearParaDto(usuario);
    }

    public async Task<IEnumerable<UsuarioDto>> ObterTodosAsync()
    {
        var usuarios = await _usuarioRepository.ObterTodosAsync();
        return usuarios.Select(MapearParaDto);
    }

    private UsuarioDto MapearParaDto(Usuario usuario)
    {
        return new UsuarioDto
        {
            IdUsuario = usuario.IdUsuario,
            NomeUsuario = usuario.NomeUsuario,
            EmailUsuario = usuario.EmailUsuario,
            TipoUsuario = usuario.TipoUsuario,
            DataCadastroUsuario = usuario.DataCadastroUsuario,
            DataNascimentoUsuario = usuario.DataNascimentoUsuario,
            SexoUsuario = usuario.SexoUsuario,
            CepUsuario = usuario.CepUsuario,
            EnderecoUsuario = usuario.EnderecoUsuario,
            CidadeUsuario = usuario.CidadeUsuario,
            TelefoneUsuario = usuario.TelefoneUsuario
        };
    }
}