using LojaDoManoel.Api.Models;

namespace LojaDoManoel.Api.Services
{
    public class DimensoesCaixaService
    {
        public int Altura { get; private set; }
        public int Largura { get; private set; }
        public int Comprimento { get; private set; }

        private int VolumeRestante;

        public DimensoesCaixaService(Caixa caixa)
        {
            Altura = caixa.Altura;
            Largura = caixa.Largura;
            Comprimento = caixa.Comprimento;
            VolumeRestante = caixa.Volume;
        }

        private int[] OrdenarDimensoes(int a, int b, int c)
        {
            var dims = new[] { a, b, c };
            Array.Sort(dims);
            return dims;
        }

        public bool CabeProduto(Produto produto)
        {
            var dimsProduto = OrdenarDimensoes(produto.Altura, produto.Largura, produto.Comprimento);
            var dimsCaixa = OrdenarDimensoes(Altura, Largura, Comprimento);

            for (int i = 0; i < 3; i++)
            {
                if (dimsProduto[i] > dimsCaixa[i])
                    return false;
            }

            return VolumeRestante >= produto.Volume;
        }

        public void RemoverProduto(Produto produto)
        {
            VolumeRestante -= produto.Volume;
            if (VolumeRestante < 0)
                VolumeRestante = 0;
        }
    }
}
