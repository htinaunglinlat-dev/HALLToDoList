using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HALLToDoList.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblList> TblLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            string connectionString = "Data Source=DESKTOP-UST9CM1\\SQLEXPRESS;Initial Catalog=ToDoList;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblList>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK_toDoLists");

            entity.ToTable("Tbl_Lists");

            entity.Property(e => e.Status).HasMaxLength(10);
            entity.Property(e => e.TaskTitle).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
