#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Project.Models;

    public class Login
{
    
    [Required]    
    public string Email { get; set; }    
    [Required]
    [DataType(DataType.Password)]    
    public string Password { get; set; } 
}