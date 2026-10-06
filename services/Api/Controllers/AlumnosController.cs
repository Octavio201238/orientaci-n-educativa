using System.Threading.Tasks;
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
    public async Task<IActionResult>ListarTodos()
    {
        var alumnos = await this.repository.ListarTodos();
        return Ok(alumnos);
    }
}

    