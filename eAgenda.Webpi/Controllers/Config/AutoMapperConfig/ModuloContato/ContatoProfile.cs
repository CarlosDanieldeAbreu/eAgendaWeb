using AutoMapper;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webpi.ViewModels.ModuloContato;

namespace eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloContato
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            ConverterDeModelParaEntidade();
            ConverterDeEntidadeParaViewModel();
        }
        private void ConverterDeModelParaEntidade()
        {
            CreateMap<InserirContatoViewModel, Contato>();
            CreateMap<EditarContatoViewModel, Contato>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {

            CreateMap<Contato, ListarContatoViewModel>();
            CreateMap<Contato, VisualizarContatoViewModel>();
        }
    }
}
