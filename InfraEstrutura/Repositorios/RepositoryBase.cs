using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; //entityframework..
using Projeto.InfraEstrutura.Contexto; //DataContext..
using Projeto.Domain.Contratos.Repositorios;

namespace Projeto.InfraEstrutura.Repositorios
{
    public abstract class RepositoryBase<TEntity>
        : IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected DataContext Con { get; set; }
        protected DbContextTransaction Tr { get; set; }

        public RepositoryBase()
        {
            Con = new DataContext();
        }

        public void BeginTransaction()
        {
            Tr = Con.Database.BeginTransaction();
        }

        public void Commit()
        {
            Tr.Commit();
        }

        public void Delete(TEntity obj)
        {            
            Con.Entry(obj).State = EntityState.Deleted; //excluindo..
            Con.SaveChanges(); //executando..

        }

        public List<TEntity> FindAll()
        {
            return Con.Set<TEntity>().ToList();
        }

        public TEntity FindById(int id)
        {
            return Con.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity obj)
        {
            Con.Entry(obj).State = EntityState.Added; //inserindo..
            Con.SaveChanges(); //executando.            

        }

        public void Rollback()
        {
            Tr.Rollback();

        }

        public void Update(TEntity obj)
        {
            Con.Entry(obj).State = EntityState.Modified; //atualizando..
            Con.SaveChanges(); //executando..

        }
    }
}
