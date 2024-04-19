using SaldoFacil.Model.Auxiliares;
using SaldoFacil.Model.Dtos;

namespace SaldoFacil.Servicos
{
    public class ServicoTransacao: IServicoTransacao
    {
        private readonly IConfiguration _configuracao;
        private readonly IServicoProvedorHttpClient _httpClient;
        private readonly string _urlTransacao;

        public ServicoTransacao(IServicoProvedorHttpClient httpClient, IConfiguration configuracao)
        {
            _httpClient = httpClient;
            _urlTransacao = configuracao["Conexao:TransacaoApi:Url"];
        }

        public async Task Transferencia(int clienteRemetente, int destinatarioId, decimal valor)
        {
            if (clienteRemetente == destinatarioId) throw new Exception("Cliente não pode trasferir para mesma conta!");

            TransferenciaDto transferenciaDto = new TransferenciaDto();

            transferenciaDto.clienteRemetenteId = clienteRemetente;
            transferenciaDto.DestinatarioId = destinatarioId;
            transferenciaDto.Valor = valor;

            await _httpClient
                        .ConfigurarClienteHttp(_urlTransacao)
                        .EnviarAssincrono<TransferenciaDto, string>($"transacao/transferencia", transferenciaDto);
        }

        public async Task Credito(int clienteId, decimal valor)
        {
            CreditoDto creditoDto = new CreditoDto();

            creditoDto.ClienteId = clienteId;
            creditoDto.Valor = valor;

            await _httpClient
                        .ConfigurarClienteHttp(_urlTransacao)
                        .EnviarAssincrono<CreditoDto, string>($"transacao/credito", creditoDto);
        }

        public async Task Debito(int clienteId, decimal valor)
        {
            DebitoDto debitoDto = new DebitoDto();

            debitoDto.ClienteId = clienteId;
            debitoDto.Valor = valor;
            
            await _httpClient
                        .ConfigurarClienteHttp(_urlTransacao)
                        .EnviarAssincrono<DebitoDto, string>($"transacao/debito", debitoDto);
            
        }
    }
}
