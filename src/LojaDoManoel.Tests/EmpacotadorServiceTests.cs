using LojaDoManoel.Api.Data;
using LojaDoManoel.Api.Models;
using LojaDoManoel.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LojaDoManoel.Api.Tests
{
    public class EmpacotadorServiceTests
    {
        [Fact]
        public void EmpacotarPedido_DeveEmpacotarProdutosCorretamente()
        {
            // Arrange: configura DbContext InMemory para teste
            var options = new DbContextOptionsBuilder<LojaDoManoelDbContext>()
                .UseInMemoryDatabase(databaseName: "TesteEmpacotador")
                .Options;

            using var context = new LojaDoManoelDbContext(options);

            var service = new EmpacotadorService(context);

            var pedido = new Pedido
            {
                Id = "pedido1",
                Produtos = new List<Produto>
                {
                    new Produto { Id = "p1", Nome = "Produto Pequeno", Altura = 5, Largura = 10, Comprimento = 15 },
                    new Produto { Id = "p2", Nome = "Produto Médio", Altura = 10, Largura = 20, Comprimento = 25 },
                    new Produto { Id = "p3", Nome = "Produto Grande", Altura = 30, Largura = 40, Comprimento = 50 }
                }
            };

            // Act
            var resultado = service.EmpacotarPedido(pedido);

            // Assert
            Assert.NotNull(resultado);
            Assert.NotEmpty(resultado);

            // Cada resultado tem uma caixa com produtos dentro
            foreach (var caixaResultado in resultado)
            {
                Assert.NotNull(caixaResultado.Caixa);
                Assert.NotEmpty(caixaResultado.Produtos);
            }

            // Verifica se todos os produtos foram empacotados
            var produtosEmpacotados = resultado.SelectMany(r => r.Produtos).ToList();
            Assert.Equal(pedido.Produtos.Count, produtosEmpacotados.Count);

            // Opcional: verificar se algum produto ficou de fora (não empacotado)
            foreach (var produto in pedido.Produtos)
            {
                Assert.Contains(produto, produtosEmpacotados);
            }
        }
    }
}
