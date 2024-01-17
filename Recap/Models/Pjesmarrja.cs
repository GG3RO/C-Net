using System.ComponentModel.DataAnnotations;
using Recap.Models;
#pragma warning disable CS8618

public class Pjesmarrja{

    [Key]

    public int PjesmarrjaId {get; set;}
    public int? WeddingId {get; set;}

    public int? UserId {get; set;}

    public DateTime CreatedAt {get; set;} =DateTime.Now;

    public DateTime UpdatedAt {get; set;} =DateTime.Now;

    public User? Dasmori {get; set;}

    public Wedding? Dasma {get; set;}
}