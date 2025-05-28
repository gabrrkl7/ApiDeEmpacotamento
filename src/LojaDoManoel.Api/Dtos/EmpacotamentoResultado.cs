using LojaDoManoel.Api.Models;

namespace LojaDoManoel.Api.DTOs
{
    public class EmpacotamentoResultado
    {
        public Caixa Caixa { get; set; } = new();
        public List<Produto> Produtos { get; set; } = new();
    }
}
