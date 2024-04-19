using SaldoFacilTransacao.API.Configuracoes;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var injecaoDepedenciaConfiguracoes = new InjecaoDepedenciaConfiguracoes();
injecaoDepedenciaConfiguracoes.AddInjecaoDepedenciaConfig(builder.Services, configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
