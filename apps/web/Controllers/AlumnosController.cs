using Microsoft.AspNetCore.Mvc;

namespace web.Controllers;

public class AlumnosController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}