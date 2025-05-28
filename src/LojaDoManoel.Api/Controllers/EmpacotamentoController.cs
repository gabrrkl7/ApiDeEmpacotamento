using Microsoft.AspNetCore.Mvc;
using LojaDoManoel.Api.Models;
using LojaDoManoel.Api.Services;
using System.Threading.Tasks;
using System.Linq;

namespace LojaDoManoel.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpacotamentoController : ControllerBase
    {
        private readonly EmpacotadorService _service;

        public EmpacotamentoController(EmpacotadorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pedido pedido)
        {
            var empacotado = await _service.EmpacotarESalvarPedidoAsync(pedido);

            var resposta = new
            {
                PedidoId = pedido.Id,
                CaixasUsadas = empacotado.Select(e => new
                {
                    Caixa = new { e.Caixa.Altura, e.Caixa.Largura, e.Caixa.Comprimento },
                    Produtos = e.Produtos.Select(p => p.Nome)
                })
            };

            return Ok(resposta);
        }
    }
}
