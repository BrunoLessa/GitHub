using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Domain.Entidades; //Entidades..

namespace Projeto.Domain.Contratos.Repositorios
{
    public interface IRepositoryLivro:IRepositoryBase<Livro>
    {
        List<Livro> FindAll(string ordenacao);
        List<Livro> FindBy(string propiedade, string conteudo);        
    }
}
