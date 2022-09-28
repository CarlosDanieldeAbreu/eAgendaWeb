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
    public class CategoriaController : eAgendaControllerBase
    {
        private readonly ServicoCategoria servicoCategoria;
        private readonly IMapper mapeadorCategorias;

        public CategoriaController(ServicoCategoria servicoCategoria, IMapper mapeadorCategorias)
        {
            this.servicoCategoria = servicoCategoria;
            this.mapeadorCategorias = mapeadorCategorias;
        }

        [HttpGet]
        public ActionResult<List<ListarCategoriaViewModel>> SelecionarTodos()
        {
            var categoriaResult = servicoCategoria.SelecionarTodos();

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategorias.Map<List<ListarCategoriaViewModel>>(categoriaResult.Value)
            });
        }

        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarCategoriaViewModel> SelecionarPorId(Guid id)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);

            if (categoriaResult.IsFailed && RegistroNaoEncontrado(categoriaResult))
                return NotFound(categoriaResult);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorCategorias.Map<VisualizarCategoriaViewModel>(categoriaResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsCategoriaViewModel> Inserir(InserirCategoriaViewModel categoriaVM)
        {
            var categoria = mapeadorCategorias.Map<Categoria>(categoriaVM);

            var categoriaResult = servicoCategoria.Inserir(categoria);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsCategoriaViewModel> Editar(Guid id, EditarCategoriaViewModel categoriaVM)
        {
            var categoriaResult = servicoCategoria.SelecionarPorId(id);

            if (categoriaResult.IsFailed && RegistroNaoEncontrado(categoriaResult))
                return NotFound(categoriaResult);

            var categoria = mapeadorCategorias.Map(categoriaVM, categoriaResult.Value);

            categoriaResult = servicoCategoria.Editar(categoria);

            if (categoriaResult.IsFailed)
                return InternalError(categoriaResult);

            return Ok(new
            {
                sucesso = true,
                dados = categoriaVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var categoriaResult = servicoCategoria.Excluir(id);

            if (categoriaResult.IsFailed && RegistroNaoEncontrado<Categoria>(categoriaResult))
                return NotFound(categoriaResult);

            if (categoriaResult.IsFailed)
                return InternalError<Categoria>(categoriaResult);

            return NoContent();
        }
    }
}
