using Basedatos;
using BaseDatos.Documents;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BaseDatos.Repositories;

public class DocentesRepository : Repository
{
    private readonly IMongoCollection<Docente> docentes;

    public DocentesRepository(
        IMongoClient client,
        IOptions<MongoDbSettings> options
    ) : base(client, options)
    {
        var db = client.GetDatabase(options.Value.DatabaseName);
        this.docentes = db.GetCollection<Docente>("docentes");
    }

    public async Task<List<Docente>> ListarTodos()
    {
        var lista = await this.docentes.FindAsync(x => true);
        return lista.ToList();
    }
}