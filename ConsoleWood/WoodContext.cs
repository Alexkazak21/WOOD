using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ConsoleWood;

public partial class WoodContext : DbContext
{
    public WoodContext()
    {
    }

    public WoodContext(DbContextOptions<WoodContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Kub> Kubs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Timber> Timbers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=IT04733;Database=wood;User ID=sa;Password=Qwe12345;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC074CB7D841");

            entity.ToTable("Customer");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Pass).HasMaxLength(50);
        });

        modelBuilder.Entity<Kub>(entity =>
        {
            entity.ToTable("Kub");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Diameter).HasColumnName("diameter");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC0759426A7E");

            entity.ToTable(tb => tb.HasTrigger("Wood_Orders_Delete"));

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.Timber).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TimberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Timber");
        });

        modelBuilder.Entity<Timber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Timber__3214EC0753EBE9F0");

            entity.ToTable("Timber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
