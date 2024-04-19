using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace SaldoFacil.Servicos
{
    public class ServicoProvedorHttpClient : IServicoProvedorHttpClient
    {
        private HttpClient _clienteHttp;
        private string _contentType = "application/json";


        public ServicoProvedorHttpClient ConfigurarContentType(string contentType)
        {
            _contentType = contentType;

            return this;
        }

        public ServicoProvedorHttpClient ConfigurarClienteHttp(string url, int timeoutEmMinutos = 10)
        {
            _clienteHttp = new HttpClient();

            if (_clienteHttp.BaseAddress == null)
                _clienteHttp.BaseAddress = new Uri(url);

            _clienteHttp.Timeout = TimeSpan.FromMinutes(timeoutEmMinutos);
            _clienteHttp.DefaultRequestHeaders.Accept.Clear();
            _clienteHttp.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_contentType));

            return this;
        }

        public async Task EnviarAssincrono<TBody>(string uri, TBody corpo)
        {
            var parametro = new StringContent(JsonConvert.SerializeObject(corpo), Encoding.UTF8, _contentType);

            try
            {
                HttpResponseMessage resposta = await _clienteHttp.PostAsync(uri, parametro);

                resposta.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TResult> EnviarAssincrono<TBody, TResult>(string uri, TBody corpo)
        {
            var parametro = new StringContent(JsonConvert.SerializeObject(corpo), Encoding.UTF8, _contentType);
            var retorno = string.Empty;

            try
            {
                HttpResponseMessage resposta = await _clienteHttp.PostAsync(uri, parametro);

                if (!resposta.IsSuccessStatusCode)
                {
                    retorno = await resposta.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(retorno))
                    {                    
                        throw new Exception(resposta.ReasonPhrase);
                    }
                }

                resposta.EnsureSuccessStatusCode();

                retorno = resposta.Content.ReadAsStringAsync().Result;

                return await Task.Run(() => JsonConvert.DeserializeObject<TResult>(retorno));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
