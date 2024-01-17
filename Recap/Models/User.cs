#pragma warning disable CS8618
namespace Recap.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User{

    [Key]
    public int UserId {get; set;}
    
    [Required]
    public string FirstName {get;set;}
    [Required]
    public string LastName {get;set;}
    [Required]
    [MinLength(5, ErrorMessage ="Username must be 8 charachters")]
    [UniqueUsername]
    public string UserName {get;set;}
    [Required]
    [MinLength(8, ErrorMessage ="Password must be at least 8 charachters")]
    [DataType(DataType.Password)]
    public string Password {get; set;}  

    public DateTime CreatedAt {get; set;} =DateTime.Now;

    public DateTime UpdatedAt {get; set;} =DateTime.Now;

    [NotMapped]
    
    [DataType(DataType.Password)]
    [Compare("Password")]
    public string   PasswordConfirm{get; set;}

    public List<Wedding>? dasmatEKrijuara {get;set;}

    public List<Pjesmarrja>? dasmaKuMerrPjese {get;set;}
}