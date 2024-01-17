#pragma warning disable CS8618
namespace Recap.Models;
using System.ComponentModel.DataAnnotations;

public class Wedding {

    [Key]
    public int WeddingId {get; set;}

    public int? UserId {get; set;}
    public string WedderOne{get; set;}

    public string WedderTwo{get; set;}

    public int Height {get; set;}

    public DateTime Date {get; set;}

    public string Adress {get; set;}
    
    public DateTime CreatedAt {get; set;} =DateTime.Now;

    public DateTime UpdatedAt {get; set;} =DateTime.Now;

    public List<Pjesmarrja>? teFtuarit {get;set;}

    public User? Creator {get;set;}
}