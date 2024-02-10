#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project.Models;

public class FavoritePizza
{
    [Key]
    public int FavoritePizzaId { get; set; }

    public string Name { get; set; }
    public int? UserId { get; set; }
    public User? Creator { get; set; }
    public Order? Order {get;set;}
}