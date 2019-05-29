using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Domain.Contratos.Repositorios;
using Projeto.Domain.Contratos.Servicos;

namespace Projeto.Domain.Servicos
{
    public abstract class DomainServiceBase<TEntity>
        : IDomainServiceBase<TEntity>
        where TEntity : class
    {
        //Iterface para abstracao do repositorio de dados
        private readonly IRepositoryBase<TEntity> repository;

        // ctor para injecao de dependencia 
        public DomainServiceBase(IRepositoryBase<TEntity> repository)
        {
            this.repository = repository;
        }

        public virtual void Atualizar(TEntity obj)
        {
            try
            {
                repository.BeginTransaction();
                repository.Update(obj);
                repository.Commit();
            }
            catch (Exception e)
            {
                repository.Rollback();
                throw new Exception(e.Message);
            }
        }

        public virtual void Excluir(TEntity obj)
        {
            try
            {
                repository.BeginTransaction();
                repository.Delete(obj);
                repository.Commit();
            }
            catch (Exception e)
            {
                repository.Rollback();
                throw new Exception(e.Message);
            }
        }

        public virtual void Inserir(TEntity obj)
        {
            try
            {
                repository.BeginTransaction();
                repository.Insert(obj);
                repository.Commit();
            }
            catch (Exception e)
            {
                repository.Rollback();
                throw new Exception(e.Message);
            }
        }

        public TEntity ObterPorId(int id)
        {
            try
            {                        
                return repository.FindById(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TEntity> ObterTodos()
        {
            try
            {                
                return repository.FindAll(); 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
