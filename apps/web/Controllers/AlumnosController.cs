using Microsoft.AspNetCore.Mvc;

namespace web.Controllers;

[Route("alumnos")]
public class AlumnosController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

[Route("{numControl:long}")]
    public IActionResult DatosAlumnos(long numControl)
    {
        this.ViewData["NumControl"] = numControl;
        return View();
    }
}