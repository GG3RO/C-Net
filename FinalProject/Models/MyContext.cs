#pragma warning disable CS8618

using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Models;
public class MyContext : DbContext 
{   
    public MyContext(DbContextOptions options) : base(options) { }    
    public DbSet<Programi> Programi { get; set; } 
    public DbSet<User> Users {get;set;}
    public DbSet<Pjesemarrje> Pjesemarrjet{get; set;}
    public DbSet<Instructor> Instructor {get; set;}
   
}