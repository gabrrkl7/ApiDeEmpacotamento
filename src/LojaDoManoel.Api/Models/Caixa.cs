namespace LojaDoManoel.Api.Models;

public class Caixa
{
    public string Nome { get; set; }
    public int Altura { get; set; }
    public int Largura { get; set; }
    public int Comprimento { get; set; }

    public int Volume => Altura * Largura * Comprimento;
}