using Basedatos;
using BaseDatos.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BaseDatosTest;

public class AlumnosRepositoryTest
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
    
    
    services.AddTransient<AlumnosRepository>();

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
[Test]
    public async  Task ObtieneDatosAlumnos()
    {
        var repository = this.serviceProvider.GetRequiredService<AlumnosRepository>();
        var list = await repository.ListarTodos();

        int cuantosAlumnos = list.Count;
        Assert.That(cuantosAlumnos > 0, "No contiene alumnos");
        Assert.That(cuantosAlumnos == 51, "No se trajo todos los registros");
    }
[Test]
    public async Task ObtieneUnAlumnoYValidaSuNombre()
    {
        long numControl = 25328051050267;
        string apellido = "Ortega";
        var repository = this.serviceProvider.GetRequiredService<AlumnosRepository>();
        var alumno = await repository.ObtenerAlumnoPorNumeroDeControl(numControl);

        Assert.That(alumno is not null, $"No existe un alumno con el numero de control {numControl}");
        if(alumno != null)
        {
            Assert.That
            (string.Equals(alumno.Paterno, "Ortega",
             StringComparison.OrdinalIgnoreCase),
              $"El apellido esperado es {apellido} y el registro obtuvo {alumno.Paterno}");
            
        }
    }
}