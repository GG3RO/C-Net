using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ViewModelFun.Models;

namespace ViewModelFun.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        return View();
    }

    public IActionResult Numbers()
    {
        List<int> Num =new List<int>{
            1, 2, 10, 21 ,8,7,3
        };
        return View("Numbers", Num);
    }
    public IActionResult User(){
        User newUser= new User(){
            FirstName ="Neil" ,
            LastName = "Graman"
        };


        return View("User", newUser  );
    }
        public IActionResult Users(){
            List<string> names = new List<string>(){"Neil Gamain", "Terry Pratchet", "Jane Austen", "Stephen King", "Mary Shelley"

            };
        return View( names );
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
