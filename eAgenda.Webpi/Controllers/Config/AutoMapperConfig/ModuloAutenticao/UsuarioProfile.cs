using AutoMapper;
using eAgenda.Dominio.ModuloAutenticacao;
using eAgenda.Webpi.ViewModels.ModuloAutenticacao;

namespace eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloAutenticao
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<RegistrarUsuarioViewModel, Usuario>()
                .ForMember(destino => destino.EmailConfirmed, opt => opt.MapFrom(origem => true))
                .ForMember(destino => destino.UserName, opt => opt.MapFrom(origem => origem.Email));
        }
    }
}
