using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using demo3.Models;
using Type = demo3.Models.Type;

namespace demo3.Context;

public partial class Demo3Context : DbContext
{
    public Demo3Context()
    {
    }

    public Demo3Context(DbContextOptions<Demo3Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Salehistory> Salehistories { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=89.110.53.87;Port=5522;Database=demo3;Username=postgres;password=QWEasd123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.Agentid).HasName("agent_pk");

            entity.ToTable("agent");

            entity.Property(e => e.Agentid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("agentid");
            entity.Property(e => e.Address)
                .HasColumnType("character varying")
                .HasColumnName("address");
            entity.Property(e => e.Dirname)
                .HasColumnType("character varying")
                .HasColumnName("dirname");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Imagepath)
                .HasColumnType("character varying")
                .HasColumnName("imagepath");
            entity.Property(e => e.Inn)
                .HasColumnType("character varying")
                .HasColumnName("inn");
            entity.Property(e => e.Kpp)
                .HasColumnType("character varying")
                .HasColumnName("kpp");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasColumnType("character varying")
                .HasColumnName("phone");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Typeid).HasColumnName("typeid");

            entity.HasOne(d => d.Type).WithMany(p => p.Agents)
                .HasForeignKey(d => d.Typeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("agent_fk");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Productid).HasName("product_pk");

            entity.ToTable("product");

            entity.Property(e => e.Productid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("productid");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Salehistory>(entity =>
        {
            entity.HasKey(e => e.Saleid).HasName("salehistory_pk");

            entity.ToTable("salehistory");

            entity.Property(e => e.Saleid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("saleid");
            entity.Property(e => e.Agentid).HasColumnName("agentid");
            entity.Property(e => e.Saledate).HasColumnName("saledate");

            entity.HasOne(d => d.Agent).WithMany(p => p.Salehistories)
                .HasForeignKey(d => d.Agentid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("salehistory_fk");

            entity.HasMany(d => d.Products).WithMany(p => p.Sales)
                .UsingEntity<Dictionary<string, object>>(
                    "Saleofproduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("saleofproduct_fk_1"),
                    l => l.HasOne<Salehistory>().WithMany()
                        .HasForeignKey("Saleid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("saleofproduct_fk"),
                    j =>
                    {
                        j.HasKey("Saleid", "Productid").HasName("saleofproduct_pk");
                        j.ToTable("saleofproduct");
                        j.IndexerProperty<int>("Saleid").HasColumnName("saleid");
                        j.IndexerProperty<int>("Productid").HasColumnName("productid");
                    });
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.HasKey(e => e.Typeid).HasName("type_pk");

            entity.ToTable("type");

            entity.Property(e => e.Typeid)
                .UseIdentityAlwaysColumn()
                .HasColumnName("typeid");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
