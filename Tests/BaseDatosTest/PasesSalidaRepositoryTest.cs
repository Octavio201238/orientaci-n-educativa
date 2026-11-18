



using Basedatos;
using BaseDatos.Documents;
using BaseDatos.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BaseDatosTest;




public class PaseSalidaRepositoryTest
{
    private ServiceProvider serviceProvider = null!;
    [SetUp]
    public void Setup()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .Build();




        var services = new ServiceCollection();




         services.AddSingleton<IConfiguration>(configuration);
        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDb"));




        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });




        services.AddTransient<PasesSalidaRepository>();




        this.serviceProvider = services.BuildServiceProvider();
    }




    [TearDown]
    public void TearDown()
    {
        if (this.serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }
    }




    //1.Listar pases de salida para un alumno existente
    [Test]
    public async Task ListarPasesAlumno()
    {
        var repository = this.serviceProvider.GetRequiredService<PasesSalidaRepository>();
        var list = await repository.ListarPasesAlumno(24328051052222);
        int cantidadPasesEsparados = 7;




        int cantidadPasesObtenidos = list.Count;




        Assert.That(cantidadPasesEsparados == cantidadPasesObtenidos, $"se esperaban {cantidadPasesEsparados} registros y se obtuvieron {cantidadPasesObtenidos} de la base de datos");
    }
    [Test]
    public async Task ObtenerPase()
    {
        var repository = this.serviceProvider.GetRequiredService<PasesSalidaRepository>();
        ObjectId id = ObjectId.Parse("6afc748511575c336b413078");
        var pase = await repository.Consultar(id);
        Assert.That(pase is not null, "no se encontro el pase con el ID proporcionado");
    }




    [Test]
    public async Task Insertar()
    {
        var repository = this.serviceProvider.GetRequiredService<PasesSalidaRepository>();
        PaseSalida pase = new PaseSalida();
        pase.Carrera = "Electronica";
        pase.Fecha = new DateTime(2026, 11, 1, 9, 15, 0);
        pase.Grupo = "E";
        pase.Motivo = "cuestiones familiares";
        pase.NombreTutor = "Sr. Juan Perez";
        pase.NumControl = 24328051052222;
        pase.Parentesco = "padre";
        pase.Semestre = 1;
        pase.Telefono = "8331234567";




        await repository.Insertar(pase);
    }
    [Test]
    public async Task Modificar()
    {
        var repository = this.serviceProvider.GetRequiredService<PasesSalidaRepository>();
        PaseSalida pase = new()
        {
            Id = ObjectId.Parse("6afc65759d361b4506dd72dc"),
            Fecha = new DateTime(2026, 10, 30, 9, 15, 0),
            Motivo = "cuestiones familiares",
            NombreTutor = "Sr. Juan Perez",
            Parentesco = "padre",
            Telefono = "8331234587",
            Comentarios = " el padre comento que saldrian de viaje por las festividades de dia de muertos"
        };
        await repository.Modificar(pase);
    }




    [Test]
    public async Task Eliminar()
    {
         var repository = this.serviceProvider.GetRequiredService<PasesSalidaRepository>();
         ObjectId id = ObjectId.Parse("6afc65759d361b4506dd72dc");




         await repository.Eliminar(id);
    }
}