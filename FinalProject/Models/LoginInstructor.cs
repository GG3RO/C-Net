#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using FinalProject.Models;

    public class LoginInstructor
{
    
    [Required]    
    public string Username { get; set; }    
    [Required]
    [DataType(DataType.Password)]    
    public string Password { get; set; } 
}