using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webpi.ViewModels.ModuloDespesa
{
    public class FormsCategoriaViewModel
    {
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string Titulo { get; set; }
    }

    public class InserirCategoriaViewModel : FormsCategoriaViewModel
    {

    }

    public class EditarCategoriaViewModel : FormsCategoriaViewModel
    {

    }
}
