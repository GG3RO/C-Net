// This brings all the MVC features we need to this file
using Microsoft.AspNetCore.Mvc;
// Be sure to use your own project's namespace here! 
namespace DojoSurvey.Controllers;   
public class DojoController : Controller   // Remember inheritance?    
{      
    [HttpGet("")] // We will go over this in more detail on the next page    

    public ViewResult Index()        
    {            
    	return   View ();   
    }  

    [HttpPost("results")]  
    public IActionResult results(string Name, string Location, string Language, string Comment)
    {
        ViewBag.Name=Name;
        ViewBag.Location=Location;
        ViewBag.Language=Language;
        ViewBag.Comment=Comment;
        return View();


    }
}