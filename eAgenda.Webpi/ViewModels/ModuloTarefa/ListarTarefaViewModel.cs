using System;

namespace eAgenda.Webpi.ViewModels.ModuloTarefa
{
    public class ListarTarefaViewModel
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; }

        public string Prioridade { get; set; }

        public string Situacao { get; set; }

    }
}
