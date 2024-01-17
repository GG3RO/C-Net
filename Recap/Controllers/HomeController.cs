using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Recap.Models;

namespace Recap.Controllers;
// Name this anything you want with the word "Attribute" at the end
public class SessionCheckAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // Find the session, but remember it may be null so we need int?
        int? userId = context.HttpContext.Session.GetInt32("UserId");
        // Check to see if we got back null
        if(userId == null)
        {
            // Redirect to the Index page if there was nothing in session
            // "Home" here is referring to "HomeController", you can use any controller that is appropriate here
            context.Result = new RedirectToActionResult("Auth", "Home", null);
        }
    }
}



public class HomeController : Controller

{
    private readonly ILogger<HomeController> _logger;

    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }
    [SessionCheck]
    public IActionResult Index()
    {
        int? userId = HttpContext.Session.GetInt32("UserId");
        ViewBag.userId = userId;

        ViewBag.AllDasmatToList=_context.Weddings.Include(e=> e.Creator).Include(e=>e.teFtuarit).ToList();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet("Auth")]
    public IActionResult Auth()
    {
        return View();
    }

    [HttpPost("Register")]
    public IActionResult Register(User User)
    {
        if (ModelState.IsValid)
        {
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            User.Password = Hasher.HashPassword(User, User.Password);
            _context.Add(User);
            _context.SaveChanges();
            return RedirectToAction("Auth");
        }
        return View("Auth");
    }

    [HttpPost("Login")]
    public IActionResult Login(LoginUser loginUser)
    {

        if (ModelState.IsValid)
        {
            User? userInDb = _context.Users.FirstOrDefault(u => u.UserName == loginUser.LoginUsername);
            if (userInDb == null)
            {
                ModelState.AddModelError("LoginUsername", "Invalid LoginUsername/LoginPassword");
                return View("Auth");
            }
            PasswordHasher<LoginUser> Hasher = new PasswordHasher<LoginUser>();
            var result = Hasher.VerifyHashedPassword(loginUser, userInDb.Password, loginUser.LoginPassword);
            if (result == 0)
            {
                ModelState.AddModelError("LoginPassword", "Invalid Password");
                return View("Auth");
            }
            else{
                HttpContext.Session.SetInt32("UserId",userInDb.UserId );
                return RedirectToAction("Index");
            }
            
        }
        return View("Auth");
    }
    [HttpGet("Logout")]
    public IActionResult Logout(){
        HttpContext.Session.Clear();
        return RedirectToAction("Auth");
    }
    [SessionCheck]
    [HttpGet("PlanWedding")]
    public IActionResult PlanWedding(){

        return View();
    }
    [HttpPost("AddWedding")]
    public IActionResult AddWedding(Wedding wedding){
        int? userId = HttpContext.Session.GetInt32("UserId");
        ViewBag.userId = userId;
        if (ModelState.IsValid)
        {
            wedding.UserId=userId;
            _context.Add(wedding);
            _context.SaveChanges();
            return RedirectToAction("Index");
        
        }
        return View("PlanWedding", wedding);
    }

    [HttpGet("Weddings/{id}")]
    public IActionResult WeddingDetails(int id){
        int? userId=HttpContext.Session.GetInt32("UserId");
        ViewBag.userId= userId;

        Wedding wedding =_context.Weddings.Include(e=> e.teFtuarit).Include(e=>e.Creator).FirstOrDefault(e=>e.WeddingId== id);
        
        
        return View("WeddingDetails" , wedding);

    }
        [SessionCheck]
        [HttpGet("AttendWedding")]
        public IActionResult AttendWedding(int Id){
        int? UserId = HttpContext.Session.GetInt32("UserId");
        ViewBag.userId = UserId;

        Pjesmarrja pjesmarrjaFromDb = new Pjesmarrja
        {
            WeddingId = Id,
            UserId = HttpContext.Session.GetInt32("UserId")
        };

        _context.Add(pjesmarrjaFromDb);
        _context.SaveChanges();
        return RedirectToAction("Index");
        }
    [HttpGet("DoNotAttendWedding")]
    public IActionResult DoNotAttendWedding(int Id){
        int? UserId = HttpContext.Session.GetInt32("UserId");
        ViewBag.userId = UserId;
        
        Pjesmarrja pjesmarrjaFromDb = _context.pjesmarrjet.FirstOrDefault(e=> e.WeddingId == Id && e.UserId == UserId);
        _context.Remove(pjesmarrjaFromDb);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
    [HttpGet("DeleteWedding")]
    public IActionResult DeleteWedding(int id){
        int? UserId = HttpContext.Session.GetInt32("UserId");
        ViewBag.userId = UserId;

        Wedding deleteWedding =_context.Weddings.Include(e => e.teFtuarit).FirstOrDefault(e=> e.WeddingId == id);
        _context.Remove(deleteWedding);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
