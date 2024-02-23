#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using FinalProject.Models;
namespace FinalProject.Models;
public class Programi
{
    [Key]
    public int ProgramiId { get; set; }
    public int? UserId {get;set;}
    [Required]
    [UniqueUsername]
    public string Name { get; set; } 
    [MinLength(4,ErrorMessage = "Descrition  Must be more then 4 characters")]
    public string Description { get; set; }
    public string? YoutubeUrl { get; set; }
    public double? AverageRating { get; set; } = 0;
    public int? TotalRaters { get; set; } = 0;
    public int? TotalRatingValues { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public Instructor? Creator {get;set;}
    public int? InstructorId {get;set;}

    public List<Pjesemarrje>? Pjesemarresit {get;set;}
}



public class UniqueUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
        {
            return new ValidationResult("Username is required!");
        }
    
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
    	if(_context.Users.Any(e => e.Username == value.ToString()))
        {
            return new ValidationResult("Username must be unique!");
        } else {
            return ValidationResult.Success;
        }
    }
}


public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        if (value is DateTime dateTime)
        {
            return dateTime > DateTime.Now;
        }
        return false; 
    }
}
