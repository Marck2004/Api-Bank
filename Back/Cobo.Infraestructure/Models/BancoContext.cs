﻿using System;
using System.Collections.Generic;
using Cobo.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Cobo.Domain.Models;

public partial class BancoContext : DbContext
{
    public BancoContext()
    {
    }

    public BancoContext(DbContextOptions<BancoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Transacction> Transacctions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Banco;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Account");

            entity.Property(e => e.Balance).HasPrecision(18, 0);
            entity.Property(e => e.NumCuenta)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany()
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cuenta_Usuarios");
        });

        modelBuilder.Entity<Transacction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Transacciones");

            entity.ToTable("Transacction");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cant).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.NumCuentaDst)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumCuentaOrg)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Usuarios");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Dni)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Passwd)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}