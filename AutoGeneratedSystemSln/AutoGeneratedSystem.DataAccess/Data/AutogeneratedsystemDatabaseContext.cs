﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using AutoGeneratedSystem.DataAccess.Models;

namespace AutoGeneratedSystem.DataAccess.Data
{
    public partial class AutogeneratedsystemDatabaseContext : DbContext
    {
        public AutogeneratedsystemDatabaseContext()
        {
        }

        public AutogeneratedsystemDatabaseContext(DbContextOptions<AutogeneratedsystemDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationRole> ApplicationRole { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<ApplicationUserApplicationRole> ApplicationUserApplicationRole { get; set; }
        public virtual DbSet<ApplicationUserOrder> ApplicationUserOrder { get; set; }
        public virtual DbSet<ApplicationUserOrderDetail> ApplicationUserOrderDetail { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Scaffolding:ConnectionString", "Data Source=(local);Initial Catalog=AutoGeneratedSystem.Database;Integrated Security=true");

            modelBuilder.Entity<ApplicationUserApplicationRole>(entity =>
            {
                entity.HasOne(d => d.ApplicationRole)
                    .WithMany(p => p.ApplicationUserApplicationRole)
                    .HasForeignKey(d => d.ApplicationRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserApplicationRole_ApplicationRole");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.ApplicationUserApplicationRole)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserApplicationRole_ApplicationUser");
            });

            modelBuilder.Entity<ApplicationUserOrder>(entity =>
            {
                entity.Property(e => e.OrderDate).HasDefaultValueSql("getutcdate()");

                entity.HasOne(d => d.ApplicationUser)
                    .WithMany(p => p.ApplicationUserOrder)
                    .HasForeignKey(d => d.ApplicationUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserOrder_ApplicationUser");
            });

            modelBuilder.Entity<ApplicationUserOrderDetail>(entity =>
            {
                entity.HasOne(d => d.ApplicationUserOrder)
                    .WithMany(p => p.ApplicationUserOrderDetail)
                    .HasForeignKey(d => d.ApplicationUserOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserOrderDetail_ApplicationUserOrder");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ApplicationUserOrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUserOrderDetail_Product");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}