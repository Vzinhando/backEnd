using AutoMapper;
using ApiDemoday.Models;
using ApiDemoday.DTOs.Usuario;

namespace ApiDemoday.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioCadastroDto, Usuario>();
            CreateMap<Usuario, UsuarioExibicaoDto>();
        }
    }
}