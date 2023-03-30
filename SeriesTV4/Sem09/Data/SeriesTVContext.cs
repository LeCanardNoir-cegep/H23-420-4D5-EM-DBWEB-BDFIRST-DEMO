using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Sem09.Models;

namespace Sem09.Data
{
    public partial class SeriesTVContext : DbContext
    {
        public SeriesTVContext()
        {
        }

        public SeriesTVContext(DbContextOptions<SeriesTVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Acteur> Acteurs { get; set; } = null!;
        public virtual DbSet<ActeurSerie> ActeurSeries { get; set; } = null!;
        public virtual DbSet<AuditActeurSerie> AuditActeurSeries { get; set; } = null!;
        public virtual DbSet<Saison> Saisons { get; set; } = null!;
        public virtual DbSet<Serie> Series { get; set; } = null!;
        public virtual DbSet<VwDetailsSerie> VwDetailsSeries { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=BDSeriesTV");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActeurSerie>(entity =>
            {
                entity.HasOne(d => d.Acteur)
                    .WithMany(p => p.ActeurSeries)
                    .HasForeignKey(d => d.ActeurId)
                    .HasConstraintName("FK_ActeurSerie_ActeurID");

                entity.HasOne(d => d.Serie)
                    .WithMany(p => p.ActeurSeries)
                    .HasForeignKey(d => d.SerieId)
                    .HasConstraintName("FK_ActeurSerie_SerieID");
            });

            modelBuilder.Entity<AuditActeurSerie>(entity =>
            {
                entity.Property(e => e.Action).IsFixedLength();

                entity.HasOne(d => d.Acteur)
                    .WithMany(p => p.AuditActeurSeries)
                    .HasForeignKey(d => d.ActeurId)
                    .HasConstraintName("FK_AuditActeurSerie_ActeurID");

                entity.HasOne(d => d.Serie)
                    .WithMany(p => p.AuditActeurSeries)
                    .HasForeignKey(d => d.SerieId)
                    .HasConstraintName("FK_AuditActeurSerie_SerieID");
            });

            modelBuilder.Entity<Saison>(entity =>
            {
                entity.HasOne(d => d.Serie)
                    .WithMany(p => p.Saisons)
                    .HasForeignKey(d => d.SerieId)
                    .HasConstraintName("FK_Saison_SerieID");
            });

            modelBuilder.Entity<VwDetailsSerie>(entity =>
            {
                entity.ToView("VW_DetailsSerie");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
