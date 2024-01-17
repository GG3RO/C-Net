#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace Recap.Models;
public class MyContext : DbContext 
{   
    public MyContext(DbContextOptions options) : base(options) { }    
    public DbSet<Wedding> Weddings { get; set;} 
    public DbSet< User> Users {get; set;}

    public DbSet<Pjesmarrja> pjesmarrjet {get; set;}
}