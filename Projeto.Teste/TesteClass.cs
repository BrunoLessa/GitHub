using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Projeto.Domain.Entidades;
using Projeto.Domain.Contratos.Servicos;
using Projeto.InfraEstrutura.Repositorios;
using Projeto.Domain.Servicos;
using System.IO;
using System.Collections.Generic;

namespace Projeto.Teste
{
    [TestClass]
    public class TesteClass
    {
        private readonly IDomainServiceLivro domain;

        public TesteClass()
        {
            domain = new DomainServiceLivro(new RepositoryLivro());
        }
        // Teste de Insercao de Dados
        [TestMethod]
        public void TestInserir()
        {            
            byte[] imageArray = System.IO.File.ReadAllBytes(@"Stuffs\livroImg.jpg");
            //string str = Convert.ToBase64String(imageArray);
            //Convert.FromBase64String(str)"
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    List<Livro> lst = new List<Livro>();
                    Livro l = new Livro()
                    {
                        Autor = "Autor" + i.ToString(),
                        Isbn = "Isbn" + i.ToString(), 
                        Titulo = "Titulo" + i.ToString(),
                        DtPublicacao = Convert.ToDateTime("10/09/2019"),
                        Preco = Convert.ToDecimal(i*100),
                        ImgLivro = imageArray

                    };

                    lst.Add(l);
                    domain.Inserir(l);
                    Assert.IsTrue(l.Id > 0);
                }

            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            
        }

        //Teste de Obter Registro sem ordenacao de campos
        [TestMethod]
        public void TestarListarTodos()
        {
            try
            {
                List<Livro> l = domain.ObterTodos();
                Assert.IsTrue(l.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        //Teste de Obter Registro ordenado pela propiedade informada
        [TestMethod]
        public void TestarListarTodosOrdenado()
        {
            try
            {
                List<Livro> l = domain.ObterTodos("DtPublicacao");
                Assert.IsTrue(l.Count > 0);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        //Teste de Obter Registro pelo conteudo informado
        [TestMethod]
        public void TestListarPorCampo()
        {            
            try
            {
                List<Livro> l = domain.ObterPor("Preco", "100");
                Assert.IsTrue(l.Count > 0);
            }
            catch (Exception e)
            {

                Assert.Fail(e.Message);
            }            
        }
        
        //Teste de autualizacao de registro
        [TestMethod]
        public void TesteAtualizar()
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(@"Stuffs\MeAndMyFriends.jpg");
            try
            {
                Livro l = new Livro()
                {
                    Id = 1,
                    Autor = "Autor2",
                    Isbn = "IsbnAutal",
                    Titulo = "Titulo",
                    DtPublicacao = Convert.ToDateTime("10/09/2019"),
                    Preco = Convert.ToDecimal("100.01"),
                    ImgLivro = imageArray
                };
                domain.Atualizar(l);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        // teste de Exclusao de registro
        [TestMethod]
        public void TestarDeleteItem()
        {
            try
            {
                Livro l = new Livro()
                {
                    Id = 1,
                    Autor = "Aba",
                    Isbn = "Isbnteste",
                    Titulo = "Aba titulo",
                    DtPublicacao = Convert.ToDateTime("10/09/2019"),
                    Preco = Convert.ToDecimal("100.01")
                };

                domain.Excluir(l);
                Assert.IsTrue(true);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

    }
}
