using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Modelo.Cadastros;


namespace Modelo.Tabelas
{
    public class Categoria
    {
        [DisplayName("Categoria")]
        public long? CategoriaId { get; set; }

        [StringLength(100, ErrorMessage = "O nome da categoria precisa ter no mínimo 10 caracteres", MinimumLength = 10)]
        [Required(ErrorMessage = "Informe o nome da categoria")]
        public string Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}