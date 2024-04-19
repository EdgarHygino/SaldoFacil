namespace SaldoFacil.Servicos
{
    public interface IServicoProvedorHttpClient
    {
        ServicoProvedorHttpClient ConfigurarClienteHttp(string url, int timeoutEmMinutos = 10);
        Task<TResult> EnviarAssincrono<TBody, TResult>(string uri, TBody corpo);
    }
}
