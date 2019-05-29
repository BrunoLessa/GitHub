
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Contratos.Repositorios
{
    public interface IRepositoryBase<TEntity>
        where TEntity:class
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Insert(TEntity obj);
        void Update(TEntity obj);
        void Delete(TEntity obj);
        List<TEntity> FindAll();
        TEntity FindById(int id);
    }
}
