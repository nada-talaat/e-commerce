﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ecommerce1.Models
{
    public partial class EcommerceContext : IdentityDbContext<ApplicationUser>
    {
        public EcommerceContext()
        {
        }

        public EcommerceContext(DbContextOptions<EcommerceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Product_Category> Product_Categories { get; set; }
        public virtual DbSet<Product_Inventory> Product_Inventories { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Ecommerce;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Color>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Discount>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Category_id)
                    .HasConstraintName("FK_Product_Product_Category");

                entity.HasOne(d => d.Discount)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Discount_id)
                    .HasConstraintName("FK_Product_Discount");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Inventory_id)
                    .HasConstraintName("FK_Product_Product_Inventory");

                //entity.HasMany(d => d.Colors)
                //    .WithMany(p => p.Products)
                //    .UsingEntity<Dictionary<string, object>>(
                //        "Product_color",
                //        l => l.HasOne<Color>().WithMany().HasForeignKey("Color_id").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Product_color_Color"),
                //        r => r.HasOne<Product>().WithMany().HasForeignKey("Product_id").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Product_color_Product"),
                //        j =>
                //        {
                //            j.HasKey("Product_id", "Color_id");

                //            j.ToTable("Product_color");
                //        });

                //entity.HasMany(d => d.Sizes)
                //    .WithMany(p => p.Products)
                //    .UsingEntity<Dictionary<string, object>>(
                //        "Product_size",
                //        l => l.HasOne<Size>().WithMany().HasForeignKey("Size_id").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Product_size_Size"),
                //        r => r.HasOne<Product>().WithMany().HasForeignKey("Product_id").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Product_size_Product"),
                //        j =>
                //        {
                //            j.HasKey("Product_id", "Size_id");

                //            j.ToTable("Product_size");
                //        });
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}