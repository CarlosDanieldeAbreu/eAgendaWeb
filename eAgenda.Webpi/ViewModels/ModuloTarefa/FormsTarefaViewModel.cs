using eAgenda.Dominio.ModuloTarefa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webpi.ViewModels.ModuloTarefa
{
    public class FormsTarefaViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public PrioridadeTarefaEnum Prioridade { get; set; }

        public List<FormsItemTarefaViewModel> Itens { get; set; }
    }

    public class InserirTarefaViewModel : FormsTarefaViewModel
    {

    }

    public class EditarTarefaViewModel : FormsTarefaViewModel
    {

    }
}
