using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LoginRegistration.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;


namespace LoginRegistration.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("Auth")]
    public IActionResult Auth()
    {
        return View("Auth");
    }

    [HttpPost("Register")]
    public IActionResult Register(User UserFromForm)
    {
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            UserFromForm.Password = Hasher.HashPassword(UserFromForm, UserFromForm.Password);
            _context.Add(UserFromForm);
            _context.SaveChanges();
            return RedirectToAction("Auth");
        }
        return View("Auth");
    }

    [HttpPost("Login")]
    public IActionResult Login(Login userSubmission)
    {
        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
            if (userInDb == null)
            {
                          
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("Auth");
            }      
            PasswordHasher<Login> hasher = new PasswordHasher<Login>();   
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);                                    // Result can be compared to 0 for failure        
            if (result == 0)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Password");            
                return View("Auth");
            }
                HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                return RedirectToAction("Index");
        }
        return View("Auth");
    }
    
    [HttpGet("LogOut")]
    public IActionResult LogOut()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Auth");
    }
    
    [SessionCheck]
    public IActionResult Index()
    {
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


public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        // Check to see if we got back null
        if (userId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Auth", "Home", null);
        }
    }
}