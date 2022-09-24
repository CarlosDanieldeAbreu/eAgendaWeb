using eAgenda.Dominio.ModuloDespesa;
using System;
using System.Collections.Generic;

namespace eAgenda.Webpi.ViewModels.ModuloDespesa
{
    public class VisualizarDespesaViewModel
    {
        public VisualizarDespesaViewModel()
        {
            Categorias = new List<VisualizarCategoriaViewModel>();
        }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public DateTime Data { get; set; }

        public FormaPgtoDespesaEnum FormaPagamento { get; set; }

        public List<VisualizarCategoriaViewModel> Categorias { get; set; }
    }
}
