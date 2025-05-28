using Microsoft.AspNetCore.Mvc;
using LojaDoManoel.Api.Models;
using LojaDoManoel.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace LojaDoManoel.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmpacotamentoController : ControllerBase
    {
        private readonly EmpacotadorService _service;

        public EmpacotamentoController(EmpacotadorService service)
        {
            _service = service;
        }

        [HttpGet("autenticado")]
        public IActionResult TesteAutenticacao()
        {
            return Ok(new { message = "Você está autenticado!" });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<Pedido> pedidos)
        {
            var resultados = new List<object>();
            
            foreach (var pedido in pedidos)
            {
                
                var resultadoEmpacotamento = await _service.EmpacotarESalvarPedidoAsync(pedido);
                
                var caixasUsadas = resultadoEmpacotamento
                    .Select(e => new {
                        Caixa = new { 
                            e.Caixa.Nome, 
                            e.Caixa.Altura, 
                            e.Caixa.Largura, 
                            e.Caixa.Comprimento 
                        },
                        Produtos = e.Produtos.Select(p => new 
                        {
                            p.Id,
                            p.Nome,
                            Dimensoes = new 
                            {
                                p.Altura,
                                p.Largura,
                                p.Comprimento
                            }
                        })
                    })
                    .ToList();
                
                resultados.Add(new
                {
                    PedidoId = pedido.Id,
                    CaixasUsadas = caixasUsadas
                });
            }
            
            return Ok(resultados);
        }
    }
}