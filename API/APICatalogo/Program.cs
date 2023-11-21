using APICatalogo.Context;
using APICatalogo.Extensions;
using APICatalogo.Filters;
using APICatalogo.Logging;
using APICatalogo.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

var builder = WebApplication.CreateBuilder(args);

//AddScoped significa que esta instancia será criada em escopo de execução, e sempre que uma nova request for criada e descartada quando terminar a request
// Será criada uma nova instancia de ApiLoggingFilter
//builder.Services.AddScoped<ApiLoggingFilter>();//Adicionando o filtro de logging que criamosa

var crateDbConection = builder.Configuration.GetConnectionString("DefaultConection");//pegando a string de conexão vindo de appsettings.json

builder.Services.AddScoped<ApiCatalogoDbSession>();

builder.Services.AddDbContext<ApiCatalogoDbContextOld>(options =>
                        options.UseNpgsql(crateDbConection));//Configurando o DbContext para utilizar o EntityFramework, também podendo ser utilizado para injeção de dependencias

//Injeta nosso padrão UnityOfWork que irá ser encarregada de acessar os Repositorys e persistir as informações no banco de dados
builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();

builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler =
                System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

////Adicionando nossos logs customizados globalmente na aplicação
//builder.Services.TryAddEnumerable(
//           ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());
//LoggerProviderOptions.RegisterProviderOptions
//    <CustomLoggerProviderConfiguration, CustomLoggerProvider>(builder.Services);

var app = builder.Build();

//Adiciona nosso middleware de tratamento de exceções configurado em ApiExceptionMiddlewareExtensions para tratar exceções globalmente
app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();//informa a API para incluir os end-points dos controladores

app.Run();
