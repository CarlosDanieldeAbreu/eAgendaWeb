using eAgenda.Dominio.ModuloContato;
using eAgenda.Webpi.ViewModels.ModuloContato;
using System;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webpi.ViewModels.ModuloCompromisso
{
    public class FormsCompromissoViewModel
    {
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string Assunto { get; set; }

        public string Local { get; set; }

        public string Link { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public TimeSpan HoraInicio { get; set; }
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public TimeSpan HoraTermino { get; set; }

        public FormsContatoViewModel Contato { get; set; }
    }

    public class InserirCompromissoViewModel : FormsCompromissoViewModel
    {

    }

    public class EditarCompromissoViewModel : FormsCompromissoViewModel
    {

    }
}
