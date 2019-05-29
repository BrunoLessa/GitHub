using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Projeto.Site.Models
{
    public class LivroModel
    {
        public int Id { get; set; }

        [Display(Name ="ISBN: ")]
        [MaxLength(50)]
        [Required(ErrorMessage = "O campo[ISBN] é de preenchimento obrigatório, favor verificar!")]
        [DataType(DataType.Text)]
        [Remote("valIsbn","Livro",ErrorMessage ="O registro informado já existe na base de dados")]
        public string Isbn { get; set; }

        [Display(Name = "Autor: ")]
        [MaxLength(50)]
        [Required(ErrorMessage = "O campo[Autor] é de preenchimento obrigatório, favor verificar!")]
        [DataType(DataType.Text)]
        public string Autor { get; set; }
        
        [Display(Name = "Titulo: ")]
        [MaxLength(50)]
        [Required(ErrorMessage = "O campo[Titulo] é de preenchimento obrigatório, favor verificar!")]
        [DataType(DataType.Text)]
        public string Titulo { get; set; }


        [Display(Name = "Preço: ")]        
        [Required(ErrorMessage = "O campo[Preco] é de preenchimento obrigatório, favor verificar!")]        
        public decimal Preco { get; set; }

        [Display(Name = "Publicação: ")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "O campo[Data publicação] é de preenchimento obrigatório, favor verificar!")] 
        [DataType(DataType.Date)]
        public DateTime DtPublicacao { get; set; }

        [Display(Name = "Imagem: ")]        
        public string ImgLivro { get; set; }

    }
}