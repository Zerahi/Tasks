using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tasks;

public partial class ProgramsContext : DbContext
{
    public ProgramsContext()
    {
    }

    public ProgramsContext(DbContextOptions<ProgramsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CostType> CostTypes { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Part> Parts { get; set; }

    public virtual DbSet<Recipy> Recipies { get; set; }

    public virtual DbSet<RecipyPart> RecipyParts { get; set; }

    public virtual DbSet<RecurringType> RecurringTypes { get; set; }

    public virtual DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Properties.Resources.ConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CostType>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Finance");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CostTypeId).HasColumnName("CostTypeID");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.RecurringTypeId).HasColumnName("RecurringTypeID");

            entity.HasOne(d => d.CostType).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.CostTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Expenses_CostTypes");

            entity.HasOne(d => d.RecurringType).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.RecurringTypeId)
                .HasConstraintName("FK_Expenses_RecurringTypes");
        });

        modelBuilder.Entity<Part>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.RecipyId).HasColumnName("RecipyID");

            entity.HasOne(d => d.Recipy).WithMany(p => p.Parts)
                .HasForeignKey(d => d.RecipyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Parts_Recipies");
        });

        modelBuilder.Entity<Recipy>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<RecipyPart>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.PartId).HasColumnName("PartID");
            entity.Property(e => e.RecipyId).HasColumnName("RecipyID");

            entity.HasOne(d => d.Part).WithMany(p => p.RecipyParts)
                .HasForeignKey(d => d.PartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecipyParts_Parts");

            entity.HasOne(d => d.Recipy).WithMany(p => p.RecipyParts)
                .HasForeignKey(d => d.RecipyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RecipyParts_Recipies");
        });

        modelBuilder.Entity<RecurringType>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
        });

        modelBuilder.Entity<Todo>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DueDate).HasColumnType("date");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
