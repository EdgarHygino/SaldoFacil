using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaldoFacil.Model.Auxiliares;
using SaldoFacil.Model.Dtos;
using SaldoFacil.Transacao.API.Servicos;

namespace SaldoFacil.Transacao.API.Controllers
{
    [Route("api/transacao")]
    [ApiController]
    public class TransacaoController : ControllerBase
    {
        private readonly IServicoTransacao _servicoTransacao;

        public TransacaoController(IServicoTransacao servicoTransacao)
        {
            _servicoTransacao = servicoTransacao;
        }

        [HttpPost("transferencia")]
        public IActionResult TrasacaoTransferencia(TransferenciaDto transferencia)
        {
            _servicoTransacao.Transferencia(transferencia);
            
            return Ok("Sucesso");
        }

        [HttpPost("credito")]
        public IActionResult TrasacaoCredito(CreditoDto creditoDto)
        {
            _servicoTransacao.Credito(creditoDto);

            return Ok("Sucesso");
        }

        [HttpPost("debito")]
        public IActionResult TrasacaoDebito(DebitoDto debitoDto)
        {
            try
            {
                _servicoTransacao.Debito(debitoDto);
                return Ok("Sucesso");
            }
            catch (SaldoInsuficienteException ex)
            {
                return StatusCode(400, "Cliente não possue saldo suficiente!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocorreu um erro durante a transação.");
            }
        }
    }
}
