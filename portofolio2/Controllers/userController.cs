
// This brings all the MVC features we need to this file
using Microsoft.AspNetCore.Mvc;
// Be sure to use your own project's namespace here! 
namespace Portofolio2.Controllers;   
public class userController : Controller   // Remember inheritance?    
{      
    [HttpGet ("")] // We will go over this in more detail on the next page  
    public ViewResult Index()        
    {  
    
        return   View();      
    }    
    [HttpGet ("Projects")]
    public ViewResult Projects()
    {
        return  View();
    }
        [HttpGet ("Contacts")]
    public ViewResult Contacts()
    {
        ViewBag.Header2 = "Contact Me";
        return  View();
    }
    
}