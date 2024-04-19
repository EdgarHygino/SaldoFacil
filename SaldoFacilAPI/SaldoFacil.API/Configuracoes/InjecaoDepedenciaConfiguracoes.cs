using Microsoft.EntityFrameworkCore;
using SaldoFacil.Transacao.API.Dados.Context;
using SaldoFacil.Transacao.API.Servicos;

namespace SaldoFacilTransacao.API.Configuracoes
{
    public class InjecaoDepedenciaConfiguracoes
    {
        public void AddInjecaoDepedenciaConfig(IServiceCollection services, IConfiguration configuration)
        {
            var db = configuration["ConnectionStrings:SaldoFacilDb"];
            services.AddDbContext<SaldoFacilDbContext>(options =>
            options.UseSqlServer(db));

            services.AddScoped<IServicoTransacao, ServicoTransacao>();
        }
    }
}
