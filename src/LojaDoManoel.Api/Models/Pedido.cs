namespace LojaDoManoel.Api.Models;

public class Pedido
{
    public string Id { get; set; }
    public List<Produto> Produtos { get; set; } = new();
}