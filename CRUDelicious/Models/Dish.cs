#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required(ErrorMessage = "The name of the Dish is required")]
    public string Name { get; set; } 
    [Required(ErrorMessage = "The name of the Chef is required")]
    public string Chef { get; set; }
    [Required(ErrorMessage = "You must rate the Tastyness from 1 to 5")]
    public int Tastyness { get; set; }
    [Required(ErrorMessage = "The field of Calories  is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Calories must be greater than 0")]
    public int Calories { get; set; }
    [Required(ErrorMessage = "The Description field is required")]
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}