using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;  

    public HomeController(ILogger<HomeController> logger , MyContext context)
    {
        _logger = logger;
        _context = context; 
    }
    public IActionResult Index()
    {
        ViewBag.dishes= _context.Dishes.OrderByDescending(e=> e.UpdatedAt).ToList();
        return View();
    }
    [HttpGet("dishes/new")]
    public IActionResult AddDish()
    {  
        
        return View();
    }
    [HttpPost]
    public IActionResult DishValid(Dish info){
        if(ModelState.IsValid)
        {
            _context.Add(info);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("AddDish");
    }
    [HttpGet("dishes/{itemId}")]
    public IActionResult Details(int itemId) {
        Dish dishFromDb = _context.Dishes.FirstOrDefault(e => e.DishId == itemId);
        return View(dishFromDb);
    }
    [HttpGet("dishes/{itemId}/edit")]
    public IActionResult Edit(int itemId) {
        Dish dishFromDb = _context.Dishes.FirstOrDefault(e => e.DishId == itemId);
        return View("Edit", dishFromDb);
    }

    [HttpPost("dishes/{itemId}/update")]
    public IActionResult Update(Dish dishFromEditForm, int itemId) {
        Dish dishFromDb = _context.Dishes.FirstOrDefault(e => e.DishId == itemId);

        if (ModelState.IsValid) {
            dishFromDb.Chef = dishFromEditForm.Chef;
            dishFromDb.Name = dishFromEditForm.Name;
            dishFromDb.Calories = dishFromEditForm.Calories;
            dishFromDb.Tastyness = dishFromEditForm.Tastyness;
            dishFromDb.Description = dishFromEditForm.Description;
            dishFromDb.UpdatedAt = dishFromEditForm.UpdatedAt;
            _context.SaveChanges();

            string referer = Request.Headers["Referer"].ToString();
            string editedReferer = referer.Replace("/edit", "");

            return Redirect(editedReferer);
        }
        return View("Edit", dishFromDb);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
