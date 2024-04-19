using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SaldoFacil.Dados.Context;
using SaldoFacil.Servicos;

namespace SaldoFacil.Configuracoes
{
    public class InjecaoDepedenciaConfiguracoes
    {
        public void AddInjecaoDepedenciaConfig(IServiceCollection services,IConfiguration configuration)
        {
            var db = configuration["ConnectionStrings:SaldoFacilDb"];
            services.AddDbContext<SaldoFacilDbContext>(options =>
            options.UseSqlServer(db));

            services.AddScoped<IServicoTransacao, ServicoTransacao>();
            services.AddScoped<IServicoProvedorHttpClient, ServicoProvedorHttpClient>();
        }
    }
}
