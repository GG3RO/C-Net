#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace productCategory.Models;
public class MyContext : DbContext 
{   
    public MyContext(DbContextOptions options) : base(options) { }    
    public DbSet<Product> Products{get; set;} 
    public DbSet<Categorie>Categories {get; set;} 
    public DbSet<Association> Associations{get; set;} 

}
