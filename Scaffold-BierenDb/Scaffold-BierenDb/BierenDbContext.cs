using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Scaffold_BierenDb
{
    public partial class BierenDbContext : DbContext
    {
        public BierenDbContext()
        {
        }

        public BierenDbContext(DbContextOptions<BierenDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bieren> Bieren { get; set; }
        public virtual DbSet<Brouwers> Brouwers { get; set; }
        public virtual DbSet<Soorten> Soorten { get; set; }
        public virtual DbSet<VwBierenMetBrouwers> VwBierenMetBrouwers { get; set; }
        public virtual DbSet<VwBierenMetSoorten> VwBierenMetSoorten { get; set; }
        public virtual DbSet<VwBrouwersBeperkt> VwBrouwersBeperkt { get; set; }
        public virtual DbSet<VwToptien> VwToptien { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=Data Source=.;Initial Catalog=BierenDb;Integrated Security=True;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bieren>(entity =>
            {
                entity.HasKey(e => e.BierNr);

                entity.Property(e => e.BierNr).ValueGeneratedNever();

                entity.Property(e => e.Naam)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BrouwerNrNavigation)
                    .WithMany(p => p.Bieren)
                    .HasForeignKey(d => d.BrouwerNr)
                    .HasConstraintName("FK_Bieren_Brouwers");

                entity.HasOne(d => d.SoortNrNavigation)
                    .WithMany(p => p.Bieren)
                    .HasForeignKey(d => d.SoortNr)
                    .HasConstraintName("FK_Bieren_Soorten");
            });

            modelBuilder.Entity<Brouwers>(entity =>
            {
                entity.HasKey(e => e.BrouwerNr);

                entity.Property(e => e.BrouwerNr).ValueGeneratedNever();

                entity.Property(e => e.Adres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrNaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gemeente)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Soorten>(entity =>
            {
                entity.HasKey(e => e.SoortNr);

                entity.Property(e => e.SoortNr).ValueGeneratedNever();

                entity.Property(e => e.Soort)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwBierenMetBrouwers>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_BierenMetBrouwers");

                entity.Property(e => e.BrNaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Naam)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwBierenMetSoorten>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_BierenMetSoorten");

                entity.Property(e => e.Naam)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Soort)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwBrouwersBeperkt>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Vw_BrouwersBeperkt");

                entity.Property(e => e.Adres)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BrNaam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gemeente)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwToptien>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_toptien");

                entity.Property(e => e.Brnaam)
                    .HasColumnName("brnaam")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Brouwernr).HasColumnName("brouwernr");

                entity.Property(e => e.Omzet).HasColumnName("omzet");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
