using eAgenda.Dominio.ModuloAutenticacao;
using System;
using Taikandi;

namespace eAgenda.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }
        public Usuario usuario { get; set; }

        public EntidadeBase()
        {
            Id = SequentialGuid.NewGuid();
        }

        public abstract void Atualizar(T registro);

    }
}
