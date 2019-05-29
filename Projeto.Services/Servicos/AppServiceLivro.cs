using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Domain.Entidades;
using Projeto.Services.Contratos;
using Projeto.Domain.Contratos.Servicos;

namespace Projeto.Services.Servicos
{
    public class AppServiceLivro:AppServiceBase<Livro>, IAppServiceLivro
    {
        private readonly IDomainServiceLivro domain;

        //construtor para injeção de dependencia..
        public AppServiceLivro(IDomainServiceLivro domain)
            : base(domain) //enviando para o construtor da superclasse
        {
            this.domain = domain;
        }

        public List<Livro> ObterPor(string campo, string ordenacao)
        {
            return domain.ObterPor(campo, ordenacao);
        }

        public List<Livro> ObterTodos(string campo)
        {
            return domain.ObterTodos(campo);
        }
    }
}
