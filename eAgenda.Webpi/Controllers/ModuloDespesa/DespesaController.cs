using AutoMapper;
using eAgenda.Aplicacao.ModuloDespesa;
using eAgenda.Dominio.ModuloDespesa;
using eAgenda.Webpi.Controllers.Compartilhado;
using eAgenda.Webpi.ViewModels.ModuloDespesa;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.Webpi.Controllers.ModuloDespesa
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DespesaController : eAgendaControllerBase
    {
        private readonly ServicoDespesa servicoDespesa;
        private readonly IMapper mapeadorDespesa;

        public DespesaController(ServicoDespesa servicoDespesa, IMapper mapeadorDespesa)
        {
            this.servicoDespesa = servicoDespesa;
            this.mapeadorDespesa = mapeadorDespesa;
        }

        [HttpGet]
        public ActionResult<List<ListarDespesaViewModel>> SelecionarTodos()
        {
            var despesaResult = servicoDespesa.SelecionarTodos();

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesa.Map<List<ListarDespesaViewModel>>(despesaResult.Value)
            });
        }

        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarDespesaViewModel> SelecionarTarefaCompletaPorId(Guid id)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);

            if (despesaResult.IsFailed && RegistroNaoEncontrado(despesaResult))
                return NotFound(despesaResult);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorDespesa.Map<VisualizarDespesaViewModel>(despesaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsDespesaViewModel> Inserir(InserirDespesaViewModel despesaVM)
        {
            var despesa = mapeadorDespesa.Map<Despesa>(despesaVM);

            var despesaResult = servicoDespesa.Inserir(despesa);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = despesaVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsDespesaViewModel> Editar(Guid id, EditarDespesaViewModel despesaVM)
        {
            var despesaResult = servicoDespesa.SelecionarPorId(id);

            if (despesaResult.IsFailed && RegistroNaoEncontrado(despesaResult))
                return NotFound(despesaResult);

            var despesa = mapeadorDespesa.Map(despesaVM, despesaResult.Value);

            despesaResult = servicoDespesa.Editar(despesa);

            if (despesaResult.IsFailed)
                return InternalError(despesaResult);

            return Ok(new
            {
                sucesso = true,
                dados = despesaVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var despesaResult = servicoDespesa.Excluir(id);

            if (despesaResult.IsFailed && RegistroNaoEncontrado<Despesa>(despesaResult))
                return NotFound(despesaResult);

            if (despesaResult.IsFailed)
                return InternalError<Despesa>(despesaResult);

            return NoContent();
        }
    }
}
