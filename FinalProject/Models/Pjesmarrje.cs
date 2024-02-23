#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FinalProject.Models;
namespace FinalProject.Models;
public class Pjesemarrje
{
    [Key]
    public int PjesemarrjeId { get; set; }
    public int? UserId {get;set;}
    public int? ProgramiId {get;set;}
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public User? Pjesemarres {get;set;}
    public Programi? Programi {get;set;}
    [NotMapped]
    public int? VleraRe {get; set;}

}