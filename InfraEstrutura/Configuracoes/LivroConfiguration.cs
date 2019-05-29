using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration; //entity..
using Projeto.Domain.Entidades;//Entidades..

namespace Projeto.InfraEstrutura.Configuracoes
{
    public class LivroConfiguration:EntityTypeConfiguration<Livro>
    {
        public LivroConfiguration()
        {
            HasKey(l => l.Id);
            Property(l => l.Id)
                .IsRequired();

            Property(l => l.Isbn)
                .HasMaxLength(50)
                .IsRequired();

            Property(l => l.Autor)
                .HasMaxLength(50)
                .IsRequired();

            Property(l => l.Titulo)
                .HasMaxLength(50)
                .IsRequired();

            Property(l => l.Preco)
                .IsRequired();

            Property(l => l.DtPublicacao)
                .IsRequired();

            Property(l => l.ImgLivro)
                .IsOptional();
        }
    }
}
