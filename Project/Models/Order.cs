#pragma warning disable CS8618
namespace Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order{
    [Key]
    public int OrderId { get; set; }

    public DateTime OrderDate { get; set; }

    public string Method {get;set;}

    public string Size {get;set;}

    public string Crust {get;set;}

    public int QTY {get;set;}

    public string TOPPINGS  {get;set;}
    
    public int TotalPrice{get; set;}

    public int? UserId {get; set;}
    public User? User {get; set;}

    public int? FavouritePizzaId {get;set;}
    public FavoritePizza? Favourite {get;set;}
    public bool IsFavorite { get; set; }
    }
