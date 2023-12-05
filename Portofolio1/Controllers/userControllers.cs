
// This brings all the MVC features we need to this file
using Microsoft.AspNetCore.Mvc;
// Be sure to use your own project's namespace here! 
namespace Portofolio1.Controllers;   
public class userController : Controller   // Remember inheritance?    
{      
    [HttpGet ("")] // We will go over this in more detail on the next page  
    public string Index()        
    {            
    	return "this is my index !";        
    }    

     [HttpGet ("Projects")]
     public string Projects(){
        return  "These  are  my Projects";

     }
        [HttpGet ("Contacts")]
     public string Contacts(){
        return  "These  are  my contacts !";

     }
     
     
}