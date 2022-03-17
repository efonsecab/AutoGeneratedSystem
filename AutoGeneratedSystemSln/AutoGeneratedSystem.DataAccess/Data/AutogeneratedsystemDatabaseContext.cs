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
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Store> Store { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Scaffolding:ConnectionString", "Data Source=(local);Initial Catalog=AutoGeneratedSystem.Database;Integrated Security=true");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}