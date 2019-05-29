using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Domain.Contratos.Servicos;

namespace Projeto.Services.Servicos
{
    public abstract class AppServiceBase<TEntity>
        where TEntity : class
    {
        //inversão de dependencia (interface da camada de dominio)
        private readonly IDomainServiceBase<TEntity> domain;

        //construtor para injeção de dependencia..
        public AppServiceBase(IDomainServiceBase<TEntity> domain)
        {
            this.domain = domain;
        }

        public void Atualizar(TEntity obj)
        {
            domain.Atualizar(obj);
        }

        public void Excluir(TEntity obj)
        {
            domain.Excluir(obj);
        }

        public void Inserir(TEntity obj)
        {
            domain.Inserir(obj);
        }

        public TEntity ObterPorId(int id)
        {
            return domain.ObterPorId(id);
        }

        public List<TEntity> ObterTodos()
        {
            return domain.ObterTodos();
        }

    }
}
