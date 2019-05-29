using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Domain.Entidades;
using Projeto.Domain.Contratos.Repositorios;
using System.Linq.Expressions;

namespace Projeto.InfraEstrutura.Repositorios
{
    public class RepositoryLivro
        : RepositoryBase<Livro>,
        IRepositoryLivro
    {
        public List<Livro> FindAll(string ordenacao)
        {
            ParameterExpression pe = Expression.Parameter(typeof(Livro), "s");
            MemberExpression me = Expression.Property(pe, ordenacao);
            //var ExpressionTree = Expression.Lambda < Func < Livro>> (me, new[] { pe }).Compile();            
            Expression conversion = Expression.Convert(Expression.Property(pe, ordenacao), typeof(object));
            var ExpressionTree = Expression.Lambda<Func<Livro, object>>(conversion, pe).Compile();
            return Con.Set<Livro>()
                .OrderBy(ExpressionTree)
                .ToList();

        }

        public List<Livro> FindBy(string propiedade, string conteudo)
        {
            ParameterExpression pe = Expression.Parameter(typeof(Livro), "s");
            MemberExpression me = Expression.Property(pe, propiedade);
            Type tipo = me.Type;

            if (!tipo.Equals(typeof(byte[])))
            {
                var convert = Convert.ChangeType(conteudo, tipo);
                ConstantExpression constant = Expression.Constant(convert, tipo);
                BinaryExpression body = Expression.Equal(me, constant);
                var ExpressionTree = Expression.Lambda<Func<Livro, bool>>(body, new[] { pe });

                return Con.Livros.Where(ExpressionTree).ToList();
            }
            else
            {
                List<Livro> l = 
                Con.Livros
                    .SqlQuery("SELECT * FROM Livro WHERE ImgLivro =convert(varbinary(max)," + conteudo + ")")
                    .ToList();
                return l;
            }


        }

    }
}
