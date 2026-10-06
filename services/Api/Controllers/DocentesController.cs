using System.Threading.Tasks;
using BaseDatos;
using BaseDatos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("docentes")]
public class DocentesController : BaseApiController
{
private readonly DocentesRepository repository;
    public DocentesController(DocentesRepository repository)
    {
        this.repository = repository; 
    }
    [HttpGet]
    public async Task<IActionResult>ListarDocentes()
    {
        await repository.ListarTodos();
        return Ok();
    }
}
