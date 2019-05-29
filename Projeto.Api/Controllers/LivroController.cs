using Projeto.Domain.Entidades;
using Projeto.Services.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Projeto.Api.Controllers
{
    [RoutePrefix("api/Livro")]
    public class LivroController : ApiController
    {
        private readonly IAppServiceLivro appLivro;
        
        //construtor para injecao de dependencia
        public LivroController(IAppServiceLivro appLivro)
        {
            this.appLivro = appLivro;
        }
        
        // api/Livro    
        [HttpGet] // Metodo que lista todos os livros sem ordem de campo    
        public HttpResponseMessage ListarLivros()
        {            
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, appLivro.ObterTodos());                                
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);                
            }
        }
        // api/Livro?campo={campo} 
        [HttpGet]// Metodo que lista todos os livros ordenados pelo nome do campo
        public HttpResponseMessage ListarLivros(string campo)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, appLivro.ObterTodos(campo));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        // api/Livro/{Id}
        [HttpGet] // Metodo que Obtem o registro pelo Id
        public HttpResponseMessage ListarLivros(int Id)
        {
            try
            {
                Livro l = appLivro.ObterPorId(Id);
                if( l != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, l);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,"Não há dados");
                }                
            }
            catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
               
            }
        }

        // api/Livro/ListarPor?campo={campo}&conteudo={conteudo}        
        [HttpGet]// Metodo que lista por qualquer campo da entidade modelo de dominio              
        [Route("ListarPor")]
        public HttpResponseMessage ListarPor(string campo, string conteudo)
        {
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK, appLivro.ObterPor(campo, conteudo));                
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // api/Livro/InsereLivro        
        [HttpPost]//Insere o registro do livro
        [Route("InsereLivro")]
        public HttpResponseMessage InsereLivro([FromBody] Livro livro)
        {
            try
            {

                if(!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                appLivro.Inserir(livro);
                return Request.CreateResponse(HttpStatusCode.OK, "Registro Inserido com sucesso!");                    
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // api/Livro/AtualizarLivro/
        [HttpPut] // Metodo para atualizar o registro
        [Route("AtualizarLivro")]
        public HttpResponseMessage AtualizarLivro([FromBody] Livro livro)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                appLivro.Atualizar(livro);
                return Request.CreateResponse(HttpStatusCode.OK, "Registro Atualizado com sucesso!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }
        // api/Livro/DeletarLivro?Id={id}
        [HttpDelete]
        [Route("DeletarLivro/{Id}")]
        public HttpResponseMessage DeletarLivro(int Id)
        {
            try
            {
                Livro l = appLivro.ObterPorId(Id);

                if( l == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }

                appLivro.Excluir(l);
                return Request.CreateResponse(HttpStatusCode.OK, "Registro Excluido com sucesso!");
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
