using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers;
public class SessionCheck1Attribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        int? InstructorId = context.HttpContext.Session.GetInt32("InstructorId");
        if(InstructorId == null)
        {
            context.Result = new RedirectToActionResult("AuthInstructor", "Instructor", null);
        }
    }
}

public class InstructorController : Controller
{
    private readonly ILogger<InstructorController> _logger;

    private MyContext _context;

    public InstructorController(ILogger<InstructorController> logger , MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("AuthInstructor")]
    public IActionResult AuthInstructor(){


        return View();
    }
    
    public IActionResult RegisterInstructor(Instructor useriNgaForma)
    {

        if (ModelState.IsValid)
        {
            PasswordHasher<Instructor> Hasher = new PasswordHasher<Instructor>();
            useriNgaForma.Password = Hasher.HashPassword(useriNgaForma, useriNgaForma.Password);
            _context.Add(useriNgaForma);
            _context.SaveChanges();
            return RedirectToAction("AuthInstructor");
        }
        ViewData["ErrorMessage"] = "Registration failed. Please check your Credentials.";
        return View("AuthInstructor");
    }


    public IActionResult LoginInstructor(LoginInstructor useriNgaForma)
    {

        if (ModelState.IsValid)
        {

            Instructor useriNgaDB = _context.Instructor.FirstOrDefault(e => e.Username == useriNgaForma.Username);
            if (useriNgaDB == null)
            {
                ViewData["ErrorMessage"] = "Login failed. Please check your Username.";
                ModelState.AddModelError("LoginUsername", "Invalid Username");
                return View("AuthInstructor");
            }
            PasswordHasher<LoginInstructor> hasher = new PasswordHasher<LoginInstructor>();
            var result = hasher.VerifyHashedPassword(useriNgaForma, useriNgaDB.Password, useriNgaForma.Password);
            if (result == 0)
            {
                ViewData["ErrorMessage"] = "Login failed. Please check your Password.";
                ModelState.AddModelError("LoginPassword", "Invalid Password");
                return View("AuthInstructor");
            }
            HttpContext.Session.SetInt32("InstructorId", useriNgaDB.InstructorId);
            return RedirectToAction("Index");

        }
        return View("AuthInstructor");

    }
    public IActionResult Logout(){
        
        HttpContext.Session.Clear();
        return RedirectToAction("AuthInstructor");
    }

[SessionCheck1]
public IActionResult Index()
{
    var programs = _context.Programi.ToList();
    ViewBag.klasat = programs;
     var instructors = _context.Instructor.ToList();
    ViewBag.instructors = instructors;
     var userId = HttpContext.Session.GetInt32("InstructorId");
    ViewBag.UserId = userId; 
    return View();
}

    [SessionCheck1]
    [HttpGet("AddProgrami")]
    public IActionResult AddProgrami(){
        var userId = HttpContext.Session.GetInt32("InstructorId");
        ViewBag.UserId = userId;
        var instruktoret = _context.Instructor.ToList();
        ViewBag.instructors = instruktoret;
            return View();
    }
    [SessionCheck1]
    [HttpGet("Instructor/ViewProgram")]
    public IActionResult ViewProgram()
    {
        var userId = HttpContext.Session.GetInt32("InstructorId");
        List<Programi> programs = _context.Programi.ToList();
        ViewBag.UserId = userId;
    return View(programs);
}
    [SessionCheck1]
    [HttpGet("EditProgram/{id}")]
public IActionResult EditProgram(int id)
{
    Programi programi1db = _context.Programi.FirstOrDefault(e => e.ProgramiId == id);
    if (programi1db == null)
    {
        return NotFound();
    }

    var instruktoret = _context.Instructor.ToList();
    ViewBag.instructors = instruktoret;

    return View(programi1db); 
}       
    [SessionCheck1]
    [HttpPost]
    public IActionResult UpdateProgram(Programi programi,int id){
        Programi programi1db = _context.Programi.FirstOrDefault(e=>e.ProgramiId == id);
        programi1db.Name = programi.Name;
        programi1db.Description = programi.Description;
        programi1db.UpdatedAt = programi.UpdatedAt;
        programi1db.YoutubeUrl = programi.YoutubeUrl;

        _context.SaveChanges();
        return RedirectToAction("Index");
    }



    [SessionCheck1]
    [HttpPost("CreateProgram")]
    public IActionResult CreateProgram(Programi programi)
    {   System.Console.WriteLine("test");
        System.Console.WriteLine(programi);
        if (ModelState.IsValid)
        {
            var userId = HttpContext.Session.GetInt32("InstructorId");
            programi.InstructorId = userId;
            _context.Add(programi);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("AddProgrami");
        
    }


[SessionCheck1]
[HttpGet("Instructor/Details/{id}")] 
public IActionResult Details(int id){
    ViewBag.UserId = HttpContext.Session.GetInt32("InstructorId"); 
    ViewBag.programi = _context.Programi.Include(e => e.Creator).Include(b => b.Pjesemarresit).ThenInclude(p => p.Pjesemarres).FirstOrDefault(e => e.ProgramiId == id);
    ViewBag.User = _context.Users.FirstOrDefault(e => e.UserId == HttpContext.Session.GetInt32("InstructorId"));
    return View();
}

[SessionCheck1]
[HttpPost]
public IActionResult DeleteProgram(int id)
{
    var program = _context.Programi.Include(e=>e.Pjesemarresit).ThenInclude(e=>e.Pjesemarres).FirstOrDefault(e => e.ProgramiId == id);
    if (program == null)
    {
        return NotFound();
    }

    _context.Programi.Remove(program);
    _context.SaveChanges();
    return RedirectToAction("Index");
}





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
