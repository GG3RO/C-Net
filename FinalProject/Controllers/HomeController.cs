using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers;
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        if(userId == null)
        {
            context.Result = new RedirectToActionResult("Auth", "Home", null);
        }
    }
}

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger , MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("Auth")]
    public IActionResult Auth(){
        ViewBag.UserId=HttpContext.Session.GetInt32("UserId"); 
        var instruktoret = _context.Instructor.ToList();
        ViewBag.instructors = instruktoret;
        var programs = _context.Programi.ToList();
        ViewBag.klasat = programs;

        return View();
    }

    public IActionResult Register(User user)
    {
        
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            user.Password = Hasher.HashPassword(user, user.Password);
            _context.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Auth");
        }
        ViewData["ErrorMessage"] = "Registration failed. Please try again.";
        return View("Auth", user);
    }

    public IActionResult Login(Login user)
    {
        if (ModelState.IsValid)
        {
            User useriNgaDB = _context.Users.FirstOrDefault(e => e.Username == user.Username);
            if (useriNgaDB == null)
            {
                ViewData["ErrorMessage"] = "Login failed. Please check your Username.";
                ModelState.AddModelError("LoginUsername", "Invalid Username");
                return View("Auth");
            }
            PasswordHasher<Login> hasher = new PasswordHasher<Login>();
            var result = hasher.VerifyHashedPassword(user, useriNgaDB.Password, user.Password);
            if (result == 0)
            {   
                
                ViewData["ErrorMessage"] = "Login failed. Please check your Password.";
                ModelState.AddModelError("LoginPassword", "Invalid Password");
                return View("Auth");
            }
            HttpContext.Session.SetInt32("UserId", useriNgaDB.UserId);
            return RedirectToAction("Index");
        }
        ViewData["ErrorMessage"] = "Login failed. Please check your credentials.";
        return View("Auth");
    }
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Auth");
    }


    [SessionCheck]
    public IActionResult Index()
    {
        ViewBag.UserId=HttpContext.Session.GetInt32("UserId");  
        var programs = _context.Programi.ToList();
        ViewBag.klasat = programs;  
        var instruktoret = _context.Instructor.ToList();
        ViewBag.instructors = instruktoret;

        return View();
    }
    
    [SessionCheck]
    public IActionResult ViewProgram()
    {
        ViewBag.UserId=HttpContext.Session.GetInt32("UserId"); 
        var programs = _context.Programi.ToList();
        return View(programs);
    }
    


[HttpPost]
public IActionResult JoinProgram(int id)
{
    int? userId = HttpContext.Session.GetInt32("UserId");
    
    if (userId == null)
    {
        return RedirectToAction("Auth");
    }

    var pjesemarrje = new Pjesemarrje
    {
        UserId = userId.Value,
        ProgramiId = id
    };
    _context.Add(pjesemarrje);
    _context.SaveChanges();
    return RedirectToAction("Details", new { id = id });
}




[SessionCheck]
[HttpGet("Home/Details/{id}")] 
public IActionResult Details(int id){
    ViewBag.UserId = HttpContext.Session.GetInt32("UserId"); 
    ViewBag.programi = _context.Programi.Include(e => e.Creator).Include(b => b.Pjesemarresit).ThenInclude(p => p.Pjesemarres).FirstOrDefault(e => e.ProgramiId == id);
    ViewBag.User = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("UserId"));
    return View();
}

    [SessionCheck]
    [HttpPost("Merrpjese/{id}")]
    public IActionResult MerrPjese( Pjesemarrje pjesemarrje,int id){
        pjesemarrje.ProgramiId = id;
        pjesemarrje.UserId=HttpContext.Session.GetInt32("UserId");
        _context.Add(pjesemarrje);
        _context.SaveChanges(); 
        return RedirectToAction("Index");
    }


    [SessionCheck]
    [HttpGet("MyClasses")]
    public IActionResult MyClasses()
    {
        
        ViewBag.UserId=HttpContext.Session.GetInt32("UserId"); 
        int? userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Auth");
        }

        var userClasses = _context.Pjesemarrjet.Include(p => p.Programi).ThenInclude(e=> e.Creator).Where(p => p.UserId == userId).ToList();

        return View(userClasses);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}