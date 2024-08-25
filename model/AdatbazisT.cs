using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tanfolyam_nyilvántartás;

namespace Tanfolyam_nyilvántartás.model
{
    public partial class AdatbazisT : DbContext
    {
        public AdatbazisT()
        {
        }

        public AdatbazisT(DbContextOptions<AdatbazisT> options)
            : base(options)
        {
        }

        public virtual DbSet<Diak> Diaks { get; set; } = null!;
        public virtual DbSet<Tanar> Tanars { get; set; } = null!;
        public virtual DbSet<Tanfolyam> Tanfolyams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Dani.DESKTOP-J0LINH7\\Desktop\\ELTE PROG.INF\\2.félév\\C#\\Tanfolyam-nyilvántartás\\Tanfolyam-nyilvántartás\\bin\\Debug\\net6.0-windows\\AdatbazisT.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diak>(entity =>
            {
                entity.ToTable("Diak");

                entity.Property(e => e.DiakId).ValueGeneratedNever();

                entity.Property(e => e.Nev).HasMaxLength(50);

                entity.Property(e => e.SzamlazasiCim).HasMaxLength(50);

                entity.Property(e => e.SzamlazasiNev).HasMaxLength(50);
            });

            modelBuilder.Entity<Tanar>(entity =>
            {
                entity.ToTable("Tanar");

                entity.Property(e => e.TanarId).ValueGeneratedNever();

                entity.Property(e => e.Nev).HasMaxLength(50);
            });

            modelBuilder.Entity<Tanfolyam>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Tanfolyam");

                entity.Property(e => e.Aktiv).HasDefaultValueSql("((0))");

                entity.Property(e => e.KezdetDatuma).HasColumnType("datetime");

                entity.Property(e => e.KoltsegPerFo).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Nev).HasMaxLength(50);

                entity.Property(e => e.TanarId).HasColumnName("TanarID");

                entity.Property(e => e.TanfolyamId).ValueGeneratedOnAdd();

                entity.Property(e => e.VegzesDatuma).HasColumnType("datetime");

                entity.HasOne(d => d.Tanar)
                    .WithMany()
                    .HasForeignKey(d => d.TanarId)
                    .HasConstraintName("FK__Tanfolyam__Tanar__5AEE82B9");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
