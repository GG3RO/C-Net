#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace ChefDish.Models;


public class Dish{
    
    [Key]
    public int DishId { get; set; }
    public string Name { get; set; } 
    public string Chef { get; set; } 
    public int Tastiness { get; set; }
    public int Calories { get; set; }

}