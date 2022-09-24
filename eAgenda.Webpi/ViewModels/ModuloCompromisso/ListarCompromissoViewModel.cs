using eAgenda.Dominio.ModuloContato;
using eAgenda.Webpi.ViewModels.ModuloContato;
using System;

namespace eAgenda.Webpi.ViewModels.ModuloCompromisso
{
    public class ListarCompromissoViewModel
    {
        public ListarCompromissoViewModel()
        {
            Contato = new VisualizarContatoViewModel();
        }

        public Guid Id { get; set; }

        public string Assunto { get; set; }

        public TimeSpan HoraInicio { get; set; }

        public TimeSpan HoraTermino { get; set; }

        public VisualizarContatoViewModel Contato { get; set; }
    }
}
