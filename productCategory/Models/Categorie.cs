#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace productCategory.Models;


public class Categorie{

    [Key]
    public int CategorieId {get; set;}

    public string Name {get; set;}

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt {get; set;}

    public List<Association>? CategoryAssociation {get;set;}= new List<Association>();

}