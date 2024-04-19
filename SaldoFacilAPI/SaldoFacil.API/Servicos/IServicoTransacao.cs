using SaldoFacil.Model.Dtos;

namespace SaldoFacil.Transacao.API.Servicos
{
    public interface IServicoTransacao
    {
        void Transferencia(TransferenciaDto transferenciaDto);
        void Credito(CreditoDto creditoDto);
        void Debito(DebitoDto debitoDto);
    }
}
