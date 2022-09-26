using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webpi.ViewModels.ModuloDespesa;

namespace eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloDespesa
{
    public class DespesaProfile : Profile
    {
        public DespesaProfile()
        {
            ConverterDeModelParaEntidade();
            ConverterDeEntidadeParaViewModel();
        }

        private void ConverterDeModelParaEntidade()
        {
            CreateMap<InserirDespesaViewModel, Despesa>();
            CreateMap<EditarDespesaViewModel, Despesa>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Despesa, ListarDespesaViewModel>();
            CreateMap<Despesa, VisualizarDespesaViewModel>();
        }
    }
}
