using Basedatos;
using BaseDatos.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BaseDatosTest; 

public class PasesSalidarepositoryTest
{
     private IServiceProvider serviceProvider;



    [SetUp]
    public void SetUp()
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
            return new MongoClient (settings.ConnectionString);
        });

        services.AddTransient<PasesSalidaRepository>();

        this.serviceProvider = services.BuildServiceProvider();
    }

    [TearDown]
    public void TearDown()
    {
        if(this.serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }

    }
    
    // 1. Listar pases de salida para un alumno existente 
    [Test]
     public async Task ListarPasesAlumno()
     {
     var repository = this.serviceProvider.GetRequiredService<PasesSalidaRepository>();
     var list = await repository.ListarPasesAlumno(25328051050267);
     int cantidadPasesEsperados = 2;

     int cantidadPasesObtenidos = list.Count;

     Assert.That(cantidadPasesEsperados == cantidadPasesObtenidos, $"se esperaban {cantidadPasesEsperados} y se obtuvieron {cantidadPasesObtenidos} registros de la base de datos");  
     }

    [Test]
 public async Task ObtenerPase()
    {
        var repository = this.serviceProvider.GetRequiredService<PasesSalidaRepository>();
        ObjectId id = ObjectId.Parse("6afc597e5e302321dd2f7eb5");
        var pase = await repository.Consultar(id);
        Assert.That(pase is not null, "No se encontro el pase con el ID proporcionado");
    }
}
