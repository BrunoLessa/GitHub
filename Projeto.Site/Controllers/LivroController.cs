using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Projeto.Site.Models;//namespace das Classes de Modelo

namespace Projeto.Site.Controllers
{
    public class LivroController : Controller
    {
        protected HttpClient http;
        protected HttpResponseMessage msg;
        protected List<LivroModel> dados;
        protected string webApi = "http://localhost:57948/";


        public ActionResult Index()
        {
            //Acessar o projeto Api
            ObterListaLivros();
            return View();
        }

        // --- Metodo para Inserir o livro no cadastro --- //
        [HttpPost]
        public ActionResult CadastrarLivro(LivroModel l, HttpPostedFileBase ArqImg)
        {
            try
            {
                if (ModelState.IsValid)
                {    
                    if(ArqImg != null)
                    {
                        // converto a imagem para byte e transformo em base64 string
                        System.IO.Stream fs = ArqImg.InputStream;
                        System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        l.ImgLivro = Convert.ToBase64String(bytes, 0, bytes.Length);
                    }

                    // Serializo em formato json e salvo via api
                    using (HttpClient http = new HttpClient())
                    {
                        string json = JsonConvert.SerializeObject(l);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                        var byteContent = new ByteArrayContent(buffer);
                        Uri end = new Uri(webApi + "Api/Livro/InsereLivro/");
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                       var Resposta = http.PostAsync(end, byteContent).Result;                     
                    }                    
                    ModelState.Clear();                    
                }

            }
            catch (Exception e)
            {
                ViewBag.mensagem = e.Message;                
            }
            
            ObterListaLivros();    
            return View("Index");
        }

        // --- Metodo para Editar o cadastro do livro  --- //
        [HttpPost]
        public ActionResult EditarLivro(LivroModel l, HttpPostedFileBase ArqImg)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ArqImg != null)
                    {
                        // converto a imagem para byte e transformo em base64 string
                        System.IO.Stream fs = ArqImg.InputStream;
                        System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        l.ImgLivro = Convert.ToBase64String(bytes, 0, bytes.Length);
                    }

                    // Serializo em formato json e salvo via api
                    using (HttpClient http = new HttpClient())
                    {
                        string json = JsonConvert.SerializeObject(l);
                        var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                        var byteContent = new ByteArrayContent(buffer);
                        Uri end = new Uri(webApi + "Api/Livro/AtualizarLivro/");
                        byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                        var Resposta = http.PutAsync(end, byteContent).Result;

                    }
                    ModelState.Clear();                    
                }

            }
            catch (Exception e)
            {
                ViewBag.mensagem = e.Message;
            }

            ObterListaLivros();
            return View("Index");
        }



        // --- Metodo para Excluir o livro selecionado  --- //
        [HttpGet] //indicar que o método recebe dados por URL (Barra de Endereço)
        public ActionResult ExcluirLivro(int Id) {
            using (http = new HttpClient())
            {               
                http.BaseAddress = new Uri(webApi + "Api/Livro/");
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var response = http.DeleteAsync(String.Format("DeletarLivro/{0}",Id)).Result;

            }
            ObterListaLivros();
            return View("Index");

        }

        // --- Metodo para Obter a lista de livros --- //
        public void ObterListaLivros()
        {
            dados = new List<LivroModel>();
            //Acessar o projeto Api
            using (HttpClient http = new HttpClient())
            {
                http.BaseAddress = new Uri(webApi);
                msg = http.GetAsync("api/Livro/").Result;
                //Ler todo o conteudo obtido pela URL acima
                if (msg.IsSuccessStatusCode)
                {
                    string conteudo = msg.Content.ReadAsStringAsync().Result;
                    dados = JsonConvert.DeserializeObject<List<LivroModel>>(conteudo);
                }
                else
                {
                    ViewBag.Mensagem = msg.StatusCode;
                }

            }

            ViewBag.Lista = dados;
        }

        // --- Metodo para validar o Isbn  do livro para insercao --- 
        public JsonResult valIsbn(string Isbn,int Id = 0)
        {
            string conteudo;
            bool retorno = true;
            dados = new List<LivroModel>();

            //Acessar o projeto Api
            using (HttpClient http = new HttpClient())
            {
                http.BaseAddress = new Uri(webApi);
                msg = http.GetAsync("api/Livro/ListarPor?campo=Isbn&conteudo="+Isbn).Result;
                conteudo = msg.Content.ReadAsStringAsync().Result;
                dados = JsonConvert.DeserializeObject<List<LivroModel>>(conteudo);                               
            }

            if (dados.Count > 0 && Id == 0)// casp seja insercao
            {
                retorno = false;                
            }else if(dados.Count > 0 && Id != 0)// caso seja alteracao
            {
                foreach (LivroModel item in dados)
                {
                    if (item.Id != Id && item.Isbn == Isbn )
                    {
                        retorno = false;
                    }
                }
            }

            return Json(retorno, JsonRequestBehavior.AllowGet);

        }
    }
}