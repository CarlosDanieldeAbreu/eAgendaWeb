using eAgenda.Dominio.ModuloDespesa;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eAgenda.Webpi.ViewModels.ModuloDespesa
{
    public class FormsDespesaViewModel
    {
        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public DateTime Data { get; set; }

        [Required(ErrorMessage = "O campo '{0}' é obrigatório")]
        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public List<FormsCategoriaViewModel> Categorias { get; set; }
    }

    public class InserirDespesaViewModel : FormsDespesaViewModel
    {

    }

    public class EditarDespesaViewModel : FormsDespesaViewModel
    {

    }
}
