namespace BaseDatos.Documents

public class Alumno
{

 public Objectid id ( get; set; )    
 public long NumControl ( get; set; )
 public string Paterno ( get; set; ) = string.Empty;

 public string Materno ( get; set; ) = string.Empty;

public string Nombre ( get; set; ) = string.Empty;

public int Generacion ( get; set; )

public string Carrera ( get; set; ) = string.Empty;

public int Semestre ( get; set; )

public string grupo ( get; set; ) = string.Empty
}