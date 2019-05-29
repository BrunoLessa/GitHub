using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Domain.Entidades;

namespace Projeto.Domain.Contratos.Servicos
{
    public interface IDomainServiceLivro: IDomainServiceBase<Livro>
    {        
        List<Livro> ObterPor(string campo,string ordenacao);
        List<Livro> ObterTodos(string ordenacao);              
    }
}
