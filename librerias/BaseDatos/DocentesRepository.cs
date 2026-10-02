using MongoDB.Driver;
using System.Collections.Generic;

namespace BaseDatos
{
    public class DocentesRepository
    {
        private readonly IMongoCollection<Docente> docentes;

        public DocentesRepository()
        {
            var cliente = new MongoClient("mongodb://localhost:27017");
            var database = cliente.GetDatabase("BaseDatos");

            docentes = database.GetCollection<Docente>("Docentes");
        }

        public List<Docente> Listar()
        {
            return docentes.Find(_ => true).ToList();
        }
    }
}