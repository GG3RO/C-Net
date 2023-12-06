// This brings all the MVC features we need to this file
using Microsoft.AspNetCore.Mvc;
// Be sure to use your own project's namespace here! 
namespace razorFun.Controllers;   
public class RazController : Controller   // Remember inheritance?    
{      
    [HttpGet("")] // We will go over this in more detail on the next page    

    public ViewResult Index()        
    {            
        return View("Index");      
    } 

}

