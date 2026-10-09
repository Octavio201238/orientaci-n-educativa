using System.Threading.Tasks;
using BaseDatos.Documents;
using BaseDatos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers; 

[Route("alumnos")]
public class AlumnosController : BaseApiController
{
    private readonly AlumnosRepository repository;
    public AlumnosController(AlumnosRepository repository)
    {
     this.repository = repository;   
    }

    [HttpGet]
    public async Task<IActionResult> ListarAlumnos()
    {
    var alumnos = await this.repository.ListarTodos();
    return Ok (alumnos);    
    }
    [HttpGet("{numControl:long}")]
    public async Task <IActionResult> ObtenerDatosAlumno(long numControl)
    {
        var alumno = await this.repository.ObtenerAlumnoPorNumeroDeControl(numControl);
        if(alumno == null)
        {
            return NotFound($"No Existe un alumno con número de control {numControl})");
        }
        return Ok(alumno);
    }
}