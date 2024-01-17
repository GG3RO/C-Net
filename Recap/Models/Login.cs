#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Recap.Models;

    public class LoginUser
{
    
    [Required]    
    public string LoginUsername { get; set; }    
    [Required]
    [DataType(DataType.Password)]    
    public string LoginPassword { get; set; } 
}