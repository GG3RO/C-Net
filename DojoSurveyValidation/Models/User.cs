#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
namespace DojoSurveyValidation.Models;

public class User{

    [Required(ErrorMessage ="Name is required")]
    [MinLength(3, ErrorMessage ="Min length should be more than 2")]
    public string Name  {get; set;}
    
    [Required]
    public string Location {get; set;}
    
    [Required]
    public string Language {get; set;}

    [MinLength(20, ErrorMessage ="Min length should be above 20")]
    public string Comment {get; set;}
}