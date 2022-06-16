using System.ComponentModel.DataAnnotations;

namespace AvProduto.Models
{
    public class Produto
    {
        [Key]
        public long ProdutoId { get; set; }

        public string Nome { get; set; }

        public string Modelo { get; set; }
        public string categoria { get; set; }
    }
}
