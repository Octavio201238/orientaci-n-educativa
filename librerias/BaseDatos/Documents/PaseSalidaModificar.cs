using MongoDB.Bson;




namespace BaseDatos.Documents;




public class PaseSalidaModificar
{
    public ObjectId Id {get; set; }
    public DateTime Fecha {get; set;}
    public string Telefono {get; set; } = string.Empty;
    public string NombreTutor {get; set; } = string.Empty;
    public string Parentesco {get; set; } = string.Empty;
    public string Motivo {get; set; } = string.Empty;
    public string? Comentarios {get; set; }
}