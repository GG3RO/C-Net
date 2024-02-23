#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject.Models;
namespace FinalProject.Models;
public class Instructor
{    
    [Key]    
    public int InstructorId { get; set; }
    
    [Required]    
    public string InstructorFirstName { get; set; }
    
    [Required]        
    public string InstructorLastName { get; set; }     
    
    
    [Required]
    public string Specialized  { get; set; }    
    
    [Required]
    
    [Range(25, 40, ErrorMessage = "Instructor Age must be between 25 and 40.")]
    public int InstructorAge { get; set; } 
    public string Username { get; set; }

    public string Password { get; set; }

    public string PasswordConfirm { get; set; }
    
    public DateTime CreatedAt {get;set;} = DateTime.Now;   
    public DateTime UpdatedAt {get;set;} = DateTime.Now;
    public List<Programi>? Programi {get;set;}
    public List<Pjesemarrje>? Pjesemarrjet {get;set;}
}