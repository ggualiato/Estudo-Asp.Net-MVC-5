using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Cadastros
{
    public class Fabricante
    {
        [DisplayName("Fabricante")]
        public long? FabricanteId { get; set; }

        [StringLength(100, ErrorMessage = "O nome do fabricante precisa ter no mínimo 10 caracteres", MinimumLength = 10)]
        [Required(ErrorMessage = "Informe o nome do fabricante")]
        public string Nome { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}