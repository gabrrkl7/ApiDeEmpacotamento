namespace LojaDoManoel.Api.Models
{
    public class Produto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }

        public int Volume => Altura * Largura * Comprimento;

        public bool CabeNaCaixa(Caixa caixa)
        {
            var produtoDims = new[] { Altura, Largura, Comprimento };
            var caixaDims = new[] { caixa.Altura, caixa.Largura, caixa.Comprimento };

            Array.Sort(produtoDims);
            Array.Sort(caixaDims);

            for (int i = 0; i < 3; i++)
            {
                if (produtoDims[i] > caixaDims[i])
                    return false;
            }

            return true;
        }
    }
}