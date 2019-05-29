using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Contratos.Servicos
{
    public interface IDomainServiceBase<TEntity>
        where TEntity : class
    {
        void Inserir(TEntity obj);
        void Atualizar(TEntity obj);
        void Excluir(TEntity obj);
        List<TEntity> ObterTodos();
        TEntity ObterPorId(int id);
    }
}
