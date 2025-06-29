using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Integrador_Web_Avanz.Models;

public partial class PwaOkContext : DbContext
{
    public PwaOkContext()
    {
    }

    public PwaOkContext(DbContextOptions<PwaOkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Consulta> Consulta { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
#warning 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("Cliente");

            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("APELLIDO");
            entity.Property(e => e.Mail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MAIL");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TELEFONO");
        });

        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => e.IdConsulta);

            entity.Property(e => e.IdConsulta).HasColumnName("ID_CONSULTA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");
            entity.Property(e => e.IdPartner).HasColumnName("ID_PARTNER");
            entity.Property(e => e.TipoConsulta)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("TIPO_CONSULTA");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK_Consulta_Cliente");

            entity.HasOne(d => d.IdPartnerNavigation).WithMany(p => p.Consulta)
                .HasForeignKey(d => d.IdPartner)
                .HasConstraintName("FK_Consulta_Partner");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.IdPartner);

            entity.ToTable("Partner");

            entity.Property(e => e.IdPartner)
                .ValueGeneratedNever()
                .HasColumnName("ID_PARTNER");
            entity.Property(e => e.Imagen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IMAGEN");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MARCA");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
