#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace productCategory.Models;


public class Product{

    [Key]

    public int ProductId{get; set;}

    public string Name {get; set;}

    public string Description {get; set;}

    public string Price { get; set;}

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt {get; set;}

    public List<Association>?  ProductAssociation {get;set ;}= new List<Association>();

}