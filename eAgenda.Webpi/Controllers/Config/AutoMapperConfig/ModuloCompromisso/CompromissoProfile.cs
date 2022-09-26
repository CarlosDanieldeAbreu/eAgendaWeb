using AutoMapper;
using eAgenda.Dominio.ModuloCompromisso;
using eAgenda.Webpi.ViewModels.ModuloCompromisso;

namespace eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloCompromisso
{
    public class CompromissoProfile : Profile
    {
        public CompromissoProfile()
        {
            ConverterDeModelParaEntidade();
            ConverterDeEntidadeParaViewModel();
        }

        private void ConverterDeModelParaEntidade()
        {
            CreateMap<InserirCompromissoViewModel, Compromisso>();
            CreateMap<EditarCompromissoViewModel, Compromisso>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {

            CreateMap<Compromisso, ListarCompromissoViewModel>();
            CreateMap<Compromisso, VisualisarCompromissoViewModel>();
        }
    }
}
