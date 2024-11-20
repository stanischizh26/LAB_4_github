﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LAB_4.Models;

public partial class SimilarProductsContext : DbContext
{
    public SimilarProductsContext()
    {
    }

    public SimilarProductsContext(DbContextOptions<SimilarProductsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Enterprise> Enterprises { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<ProductionPlan> ProductionPlans { get; set; }

    public virtual DbSet<SalesPlan> SalesPlans { get; set; }

    public virtual DbSet<ViewProductsComparison> ViewProductsComparisons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) // Проверяем, не настроен ли уже контекст
        {
            // Получение конфигурации из файла appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enterprise>(entity =>
        {
            entity.HasKey(e => e.EnterpriseId).HasName("PK__Enterpri__52DEA54687132EE3");

            entity.Property(e => e.EnterpriseId).HasColumnName("EnterpriseID");
            entity.Property(e => e.ActivityType).HasMaxLength(255);
            entity.Property(e => e.DirectorName).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.OwnershipForm).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6EDF173BC00");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("PK__ProductT__A1312F4EF5375AC7");

            entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductTypes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductTy__Produ__0880433F");
        });

        modelBuilder.Entity<ProductionPlan>(entity =>
        {
            entity.HasKey(e => e.ProductionPlanId).HasName("PK__Producti__B58D523AD4355C05");

            entity.Property(e => e.ProductionPlanId).HasColumnName("ProductionPlanID");
            entity.Property(e => e.EnterpriseId).HasColumnName("EnterpriseID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Enterprise).WithMany(p => p.ProductionPlans)
                .HasForeignKey(d => d.EnterpriseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productio__Enter__0E391C95");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductionPlans)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productio__Produ__0F2D40CE");
        });

        modelBuilder.Entity<SalesPlan>(entity =>
        {
            entity.HasKey(e => e.SalesPlanId).HasName("PK__SalesPla__97D435AD778D30F5");

            entity.Property(e => e.SalesPlanId).HasColumnName("SalesPlanID");
            entity.Property(e => e.EnterpriseId).HasColumnName("EnterpriseID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Enterprise).WithMany(p => p.SalesPlans)
                .HasForeignKey(d => d.EnterpriseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalesPlan__Enter__12FDD1B2");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesPlans)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalesPlan__Produ__13F1F5EB");
        });

        modelBuilder.Entity<ViewProductsComparison>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_ProductsComparison");

            entity.Property(e => e.EnterpriseName).HasMaxLength(255);
            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.ProductTypeName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}