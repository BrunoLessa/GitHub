using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Domain.Entidades
{
    public class Livro
    {
        public virtual int Id { get; set; }
        public virtual string Isbn { get; set; }
        public virtual string Autor { get; set; }
        public virtual string Titulo { get; set; }
        public virtual decimal Preco { get; set; }
        public virtual DateTime DtPublicacao { get; set; }
        public virtual byte[] ImgLivro { get; set; }
    }
}
