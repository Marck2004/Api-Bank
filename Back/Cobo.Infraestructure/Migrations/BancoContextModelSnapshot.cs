﻿// <auto-generated />
using System;
using Cobo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cobo.Domain.Migrations
{
    [DbContext(typeof(BancoContext))]
    partial class BancoContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cobo.Domain.Models.Transacction", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("Cant")
                        .HasColumnType("decimal(18, 0)");

                    b.Property<string>("NumCuentaDst")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NumCuentaOrg")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id")
                        .HasName("PK_Transacciones");

                    b.ToTable("Transacction", (string)null);
                });

            modelBuilder.Entity("Cobo.Infraestructure.Models.Account", b =>
                {
                    b.Property<decimal>("Balance")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.Property<DateTimeOffset>("FechaCreacion")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdUsuario")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("NumCuenta")
                        .IsRequired()
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("Cobo.Infraestructure.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Apellido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Passwd")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id")
                        .HasName("PK_Usuarios");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Cobo.Infraestructure.Models.Account", b =>
                {
                    b.HasOne("Cobo.Infraestructure.Models.User", "IdUsuarioNavigation")
                        .WithMany()
                        .HasForeignKey("IdUsuario")
                        .IsRequired()
                        .HasConstraintName("FK_Cuenta_Usuarios");

                    b.Navigation("IdUsuarioNavigation");
                });
#pragma warning restore 612, 618
        }
    }
}
