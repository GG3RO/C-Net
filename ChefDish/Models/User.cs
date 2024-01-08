#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefDish.Models;

public class User{
    
    [Key]
    public int UserId { get; set; }

    public string Name {get; set;}

    public int Age {get; set;}

    public int  Dishes {get; set;}


}