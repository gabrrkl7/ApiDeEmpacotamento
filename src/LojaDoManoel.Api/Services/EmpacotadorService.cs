using LojaDoManoel.Api.Data;
using LojaDoManoel.Api.DTOs;
using LojaDoManoel.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaDoManoel.Api.Services
{
    public class EmpacotadorService
    {
        private readonly LojaDoManoelDbContext _context;

        private readonly List<Caixa> _caixasDisponiveis = new()
        {
            new Caixa { Nome = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
            new Caixa { Nome = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
            new Caixa { Nome = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
        };

        public EmpacotadorService(LojaDoManoelDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmpacotamentoResultado>> EmpacotarESalvarPedidoAsync(Pedido pedido)
        {
            var resultado = EmpacotarPedido(pedido);

            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();

            return resultado;
        }

        public List<EmpacotamentoResultado> EmpacotarPedido(Pedido pedido)
        {
            var resultado = new List<EmpacotamentoResultado>();
            var produtosRestantes = new List<Produto>(pedido.Produtos);

            // Ordena caixas pelo volume (menor primeiro)
            var caixasOrdenadas = _caixasDisponiveis.OrderBy(c => c.Volume).ToList();

            // Ordena produtos pelo volume (maior primeiro)
            produtosRestantes = produtosRestantes.OrderByDescending(p => p.Volume).ToList();

            while (produtosRestantes.Any())
            {
                // Encontra a menor caixa que pode conter pelo menos um produto restante
                var caixa = caixasOrdenadas.FirstOrDefault(c => produtosRestantes.Any(p => p.CabeNaCaixa(c)));

                if (caixa == null)
                    throw new Exception("Nenhuma caixa disponível para os produtos restantes.");

                var empacotamento = EmpacotarProdutosNaCaixa(caixa, produtosRestantes);
                if (empacotamento.Produtos.Any())
                {
                    resultado.Add(empacotamento);
                    produtosRestantes = produtosRestantes.Except(empacotamento.Produtos).ToList();
                }
                else
                {
                    break;
                }
            }

            return resultado;
        }

        private EmpacotamentoResultado EmpacotarProdutosNaCaixa(Caixa caixa, List<Produto> produtosRestantes)
        {
            var produtosNaCaixa = new List<Produto>();
            var espacoRestante = new DimensoesCaixaService(caixa);

            foreach (var produto in produtosRestantes.ToList())
            {
                if (espacoRestante.CabeProduto(produto))
                {
                    produtosNaCaixa.Add(produto);
                    espacoRestante.RemoverProduto(produto);
                }
            }

            return new EmpacotamentoResultado
            {
                Caixa = caixa,
                Produtos = produtosNaCaixa
            };
        }
    }
}
