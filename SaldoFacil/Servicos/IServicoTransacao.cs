namespace SaldoFacil.Servicos
{
    public interface IServicoTransacao
    {
        Task Transferencia(int clienteRemetente, int remetenteId, decimal valor);
        Task Credito(int clienteId, decimal valor);
        Task Debito(int clienteId, decimal valor);
    }
}
