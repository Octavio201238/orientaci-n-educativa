using Basedatos;
using BaseDatos;
using BaseDatos.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BaseDatosTest;

public class DocentesRepositoryTest
{
    private IServiceProvider serviceProvider;
    [SetUp]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false)
        .Build();

        var services = new ServiceCollection();
        services.AddSingleton<IConfiguration>(configuration);
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));

    services.AddSingleton<IMongoClient>(sp =>
    {
        var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
        return new MongoClient(settings.ConnectionString);
    });
    
    
    services.AddTransient<DocentesRepository>();

    this.serviceProvider = services.BuildServiceProvider();
}
   [TearDown]
   public void TearDown()
    {
        if(this.serviceProvider is  IDisposable disposable)
        {
        disposable.Dispose();
        }
    }

    public void ObtieneDatosDocentes()
    {
        var repository = this.serviceProvider.GetRequiredService<DocentesRepository>();
        var list = repository.Listar();

        int cuantosDocentes = list.Count;
        Assert.That(cuantosDocentes > 0, "No contiene Docentes");
        Assert.That(cuantosDocentes == 10, "No se trajo todos los registros");
    }
}