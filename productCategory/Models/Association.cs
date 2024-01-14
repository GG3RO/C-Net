#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace productCategory.Models;


public class Association{
    
    [Key]
    public int AssociationId {get; set;}

    public int? ProductId{get; set;}

    public int? CategorieId{ get; set;}

    public Categorie? Categorie {get;set;}

    public Product? Products {get;set;}
}