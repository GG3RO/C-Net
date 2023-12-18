using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DojoSurveyValidation.Models;

namespace DojoSurveyValidation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
        
    }

    [HttpPost("results")]

    public IActionResult results(User Info){
    
    {
        if(ModelState.IsValid)
        return View(Info);
    }
    return View("Index");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
