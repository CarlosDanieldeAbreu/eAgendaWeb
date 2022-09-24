using AutoMapper;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webpi.ViewModels.ModuloDespesa;

namespace eAgenda.Webpi.Controllers.Config.AutoMapperConfig.ModuloDespesa
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            ConverterDeModelParaEntidade();
            ConverterDeEntidadeParaViewModel();
        }

        private void ConverterDeModelParaEntidade()
        {
            CreateMap<InserirCategoriaViewModel, Categoria>();
            CreateMap<EditarCategoriaViewModel, Categoria>();
        }

        private void ConverterDeEntidadeParaViewModel()
        {
            CreateMap<Categoria, ListarCategoriaViewModel>();
            CreateMap<Categoria, VisualizarCategoriaViewModel>();
        }
    }
}
