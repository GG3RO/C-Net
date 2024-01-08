using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefDish.Models;

namespace ChefDish.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context; 

    public HomeController(ILogger<HomeController> logger , MyContext context)
    {   
        _context= context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.User=_context.Users.ToList();
        return View();
    }
    [HttpGet("AddChef")]
    public IActionResult AddChef(){
        return View();
    }
    
    [HttpPost("CreateChef")]
    public IActionResult CreateChef(User ChefFromForm){
        if (ModelState.IsValid)
        {
            _context.Add(ChefFromForm);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("AddChef");
    }
    [HttpGet("AddDish")]
    public IActionResult AddDish(){
        
        return View();
    }
    
    [HttpPost("CreateDish")]
    public IActionResult CreateDish(Dish DhishFromForm){
        if (ModelState.IsValid)
        {
            _context.Add(DhishFromForm);
            _context.SaveChanges();
            return RedirectToAction("Dishes");
        }
        return View("AddDish");
    }

    [HttpGet("Dishes")]
    public IActionResult Dishes(){
        
        ViewBag.Dishes=_context.Dishes.ToList();
        return View();
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
