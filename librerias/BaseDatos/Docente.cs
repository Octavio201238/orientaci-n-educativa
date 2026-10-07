using MongoDB.Bson;

namespace BaseDatos.Documents;

public class Docente
{
    public ObjectId Id { get; set; }
    public long NumControl { get; set; }

    public string Paterno { get; set; } = string.Empty;

    public string Materno { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string Materia { get; set; } = string.Empty;

    public string Turno { get; set; } = string.Empty;

}