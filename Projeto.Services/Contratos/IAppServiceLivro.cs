using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Domain.Entidades;

namespace Projeto.Services.Contratos
{
    public interface IAppServiceLivro :IAppServiceBase<Livro>
    {
        List<Livro> ObterPor(string campo, string ordenacao);
        List<Livro> ObterTodos(string campo);
    }    
}
