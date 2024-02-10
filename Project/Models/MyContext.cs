#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace Project.Models;
public class MyContext : DbContext 
{   
    public MyContext(DbContextOptions options) : base(options) { }    

    public DbSet<User> Users {get; set;}
    public DbSet<Order> Orders {get;set;}
    public DbSet<FavoritePizza> FavoritePizzas {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<FavoritePizza>()
        .HasOne(fp => fp.Order)
        .WithOne(o => o.Favourite)
        .HasForeignKey<Order>(o => o.FavouritePizzaId);
}

}