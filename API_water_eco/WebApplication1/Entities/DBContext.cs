using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace WebApplication1.Entities;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kc> Kcs { get; set; }

    public virtual DbSet<Surfaceculture> Surfacecultures { get; set; }
    
    public virtual DbSet<Watervolume> Watervolumes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kc>(entity =>
        {
            entity.HasKey(e => e.KcId).HasName("PRIMARY");

            entity.ToTable("kc");

            entity.Property(e => e.KcId)
                .HasColumnType("int(11)")
                .HasColumnName("kc_id");
            entity.Property(e => e.KcName)
                .HasMaxLength(50)
                .HasColumnName("kc_name");
            entity.Property(e => e.KcNeed)
                .HasPrecision(15)
                .HasColumnName("kc_need");
        });

        modelBuilder.Entity<Surfaceculture>(entity =>
        {
            entity.HasKey(e => e.SurId).HasName("PRIMARY");

            entity.ToTable("surfaceculture");

            entity.HasIndex(e => e.WatId, "wat_id");

            entity.Property(e => e.SurId)
                .HasColumnType("int(11)")
                .HasColumnName("sur_id");
            entity.Property(e => e.SurUnit)
                .HasMaxLength(50)
                .HasColumnName("sur_unit");
            entity.Property(e => e.SurValue)
                .HasPrecision(15)
                .HasColumnName("sur_value");
            entity.Property(e => e.WatId)
                .HasColumnType("int(11)")
                .HasColumnName("wat_id");

            entity.HasOne(d => d.Wat).WithMany(p => p.Surfacecultures)
                .HasForeignKey(d => d.WatId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("surfaceculture_ibfk_1");
        });

        modelBuilder.Entity<Watervolume>(entity =>
        {
            entity.HasKey(e => e.WatId).HasName("PRIMARY");

            entity.ToTable("watervolume");

            entity.Property(e => e.WatId)
                .HasColumnType("int(11)")
                .HasColumnName("wat_id");
            entity.Property(e => e.WatCurrentVolume)
                .HasPrecision(15)
                .HasColumnName("wat_current_volume");
            entity.Property(e => e.WatInsee)
                .HasColumnType("int(11)")
                .HasColumnName("wat_insee");
            entity.Property(e => e.WatMaxVolume)
                .HasPrecision(15)
                .HasColumnName("wat_max_volume");
            entity.Property(e => e.WatUnit)
                .HasMaxLength(50)
                .HasColumnName("wat_unit");
            entity.Property(e => e.WatName)
                .HasMaxLength(50)
                .HasColumnName("wat_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
