using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration; //connectionstring..
using System.Data.Entity; //entityframework..
using System.Data.Entity.ModelConfiguration.Conventions; //configurações..
using Projeto.Domain.Entidades;
using Projeto.InfraEstrutura.Configuracoes;

namespace Projeto.InfraEstrutura.Contexto
{
    public class DataContext:DbContext
    {
        public DataContext()
            : base(ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString)
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        //sobrescrever o método OnModelCreating
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //redefinir convenções padrão do entityframework..
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //classe de mapeamento (fluent api)
            modelBuilder.Configurations.Add(new LivroConfiguration());
        }
        
        public DbSet<Livro> Livros { get; set; } //Livros..        
    }
}
