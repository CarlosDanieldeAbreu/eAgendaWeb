using eAgenda.Aplicacao.ModuloContato;
using eAgenda.Dominio.ModuloContato;
using eAgenda.Infra.Configs;
using eAgenda.Infra.Orm;
using eAgenda.Infra.Orm.ModuloContato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace eAgenda.Webpi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly ServicoContato servicoContato;

        public ContatosController()
        {
            var config = new ConfiguracaoAplicacaoeAgenda();

            var eAgendaDbContext = new eAgendaDbContext(config.ConnectionStrings);
            var repositorioContato = new RepositorioContatoOrm(eAgendaDbContext);
            servicoContato = new ServicoContato(repositorioContato, eAgendaDbContext);
        }

        [HttpGet]
        public List<Contato> SelecionarTodos()
        {
            var tarefaResult = servicoContato.SelecionarTodos();

            if (tarefaResult.IsSuccess)
                return tarefaResult.Value;
            return null;
        }

        [HttpGet("{id:guid}")]
        public Contato SelecionarPorId(Guid id)
        {
            var tarefaResult = servicoContato.SelecionarPorId(id);

            if (tarefaResult.IsSuccess)
                return tarefaResult.Value;
            return null;
        }

        [HttpPost]
        public Contato Inserir(Contato novoContato)
        {
            var tarefaResult = servicoContato.Inserir(novoContato);

            if (tarefaResult.IsSuccess)
                return tarefaResult.Value;
            return null;
        }

        [HttpPut("{id:guid}")]
        public Contato Editar(Guid id)
        {
            var contatoEditado = servicoContato.SelecionarPorId(id).Value;

            var tarefaResult = servicoContato.Editar(contatoEditado);

            if (tarefaResult.IsSuccess)
                return tarefaResult.Value;
            return null;
        }

        [HttpDelete("{id:guid}")]
        public void Excluir(Guid id)
        {
            var contatoSelecionado = servicoContato.SelecionarPorId(id).Value;
            servicoContato.Excluir(contatoSelecionado);
        }
    }
}
