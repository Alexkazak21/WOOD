using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WoodWebAPI.Data.Entities;

namespace WoodWebAPI.Data;

public partial class WoodDBContext : DbContext
{
    public WoodDBContext()
    {
        //Database.EnsureDeletedAsync();
       //Database.EnsureCreatedAsync();
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Timber> Timbers { get; set; }

    public virtual DbSet<Kubs> Kubs { get; set; }

    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        var dir = Directory.GetParent(Environment.CurrentDirectory)?.ToString() + "\\Database\\wood.mdf";
        optionsBuilder.UseSqlServer($"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=wood;" +
            $"Integrated Security=True;Connect Timeout=10;" +
            $"Application Intent=ReadWrite;MultipleActiveResultSets=true;"); //D:\\code\\WoodWebAPI\\Database\\wood.mdf
    }      
}
