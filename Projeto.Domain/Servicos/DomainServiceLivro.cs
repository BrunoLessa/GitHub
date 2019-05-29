using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Domain.Contratos.Repositorios;
using Projeto.Domain.Contratos.Servicos;
using Projeto.Domain.Entidades;

namespace Projeto.Domain.Servicos
{
    public class DomainServiceLivro : DomainServiceBase<Livro>,
        IDomainServiceLivro
    {
        private readonly IRepositoryLivro repository;

        public DomainServiceLivro(IRepositoryLivro repository)
            :base(repository)
        {
            this.repository = repository;
        }

        public List<Livro> ObterPor(string campo, string ordenacao)
        {
            try
            {
               return this.repository.FindBy(campo, ordenacao);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public List<Livro> ObterTodos(string ordenacao)
        {
            try
            {
                return this.repository.FindAll(ordenacao);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override void Inserir(Livro obj)
        {
            try
            {
                // validacao para saber se já existe algum registro com mesmo Isbn
                if (this.ObterPor("Isbn", obj.Isbn).Count == 0)
                {
                    base.Inserir(obj);
                }
                else
                {
                    throw new ArgumentException("O valor informado no campo [Isbn] já existe na base de dados, favor verifique!");
                }
                
            }
            catch (Exception)
            {

                throw;
            }
                        
        }

        public override void Atualizar(Livro obj)
        {
            try
            {
                // valido para ver se é possivel alterar o campo Isbn
                List<Livro> lvr = this.ObterPor("Isbn", obj.Isbn);
                foreach(Livro l in lvr)
                {
                    if (l.Isbn == obj.Isbn && l.Id != obj.Id)
                    {
                        throw new ArgumentException("O valor informado no campo [Isbn] já existe na base de dados, favor verifique!");
                    }
                }

                //valido se o registro existe na base de dados
                Livro registro = this.ObterPorId(obj.Id);
                if (registro == null)
                {
                    throw new ArgumentException("Registro não encontrado na base de dados, favor verifique!");
                }

                registro.Autor = obj.Autor;
                registro.Isbn = obj.Isbn;
                registro.Preco = obj.Preco;
                registro.Titulo = obj.Titulo;
                registro.ImgLivro = obj.ImgLivro;
                registro.DtPublicacao = obj.DtPublicacao;

                base.Atualizar(registro);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
