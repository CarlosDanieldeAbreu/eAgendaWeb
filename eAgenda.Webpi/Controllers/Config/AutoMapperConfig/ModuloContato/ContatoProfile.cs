using AutoMapper;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webpi.ViewModels.ModuloContato;

namespace eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloContato
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<FormsContatoViewModel, Contato>();

            CreateMap<Contato, ListarContatoViewModel>();

            CreateMap<Contato, VisualizarContatoViewModel>();

            CreateMap<Contato, FormsContatoViewModel>();
        }
    }
}
