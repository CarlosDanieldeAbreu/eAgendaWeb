using AutoMapper;
using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Webpi.Controllers.Compartilhado;
using eAgenda.Webpi.ViewModels.ModuloContato;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.Webpi.Controllers.ModuloContato
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ContatosController : eAgendaControllerBase
    {
        private readonly ServicoContato servicoContato;
        private readonly IMapper mapeadorContatos;

        public ContatosController(ServicoContato servicoContato, IMapper mapeadorContatos)
        {
            this.servicoContato = servicoContato;
            this.mapeadorContatos = mapeadorContatos;
        }

        [HttpGet]
        public ActionResult<List<ListarContatoViewModel>> SelecionarTodos()
        {
            var contatoResult = servicoContato.SelecionarTodos();

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorContatos.Map<List<ListarContatoViewModel>>(contatoResult.Value)
            });
        }

        [HttpGet("visualizar-completa/{id:guid}")]
        public ActionResult<VisualizarContatoViewModel> SelecionarPorId(Guid id)
        {
            var contatoResult = servicoContato.SelecionarPorId(id);

            if (contatoResult.IsFailed && RegistroNaoEncontrado(contatoResult))
                return NotFound(contatoResult);

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = mapeadorContatos.Map<VisualizarContatoViewModel>(contatoResult.Value)
            });
        }

        [HttpPost]
        public ActionResult<FormsContatoViewModel> Inserir(FormsContatoViewModel contatoVM)
        {
            var contato = mapeadorContatos.Map<Contato>(contatoVM);

            var contatoResult = servicoContato.Inserir(contato);

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new
            {
                sucesso = true,
                dados = contatoVM
            });
        }

        [HttpPut("{id:guid}")]
        public ActionResult<FormsContatoViewModel> Editar(Guid id, FormsContatoViewModel contatoVM)
        {
            var contatoResult = servicoContato.SelecionarPorId(id);

            if (contatoResult.IsFailed && RegistroNaoEncontrado(contatoResult))
                return NotFound(contatoResult);

            var contato = mapeadorContatos.Map(contatoVM, contatoResult.Value);

            contatoResult = servicoContato.Editar(contato);

            if (contatoResult.IsFailed)
                return InternalError(contatoResult);

            return Ok(new 
            {
                sucesso = true,
                dados = contatoVM
            });
        }

        [HttpDelete("{id:guid}")]
        public ActionResult Excluir(Guid id)
        {
            var contatoResult = servicoContato.Excluir(id);

            if (contatoResult.IsFailed && RegistroNaoEncontrado<Contato>(contatoResult))
                return NotFound(contatoResult);

            if (contatoResult.IsFailed)
                return InternalError<Contato>(contatoResult);

            return NoContent();
        }
    }
}
