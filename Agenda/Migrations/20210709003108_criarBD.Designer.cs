﻿// <auto-generated />
using Agenda.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Agenda.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20210709003108_criarBD")]
    partial class criarBD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Agenda.Models.Evento", b =>
                {
                    b.Property<int>("eventoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("data")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("exclusivo")
                        .HasColumnType("int");

                    b.Property<string>("local")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("participantes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("tipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("eventoId");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("Agenda.Models.Usuario", b =>
                {
                    b.Property<int>("usuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("data")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("senha")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("sexo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("usuarioId");

                    b.ToTable("UsuarioCadastro");
                });

            modelBuilder.Entity("Agenda.Models.UsuariosEventos", b =>
                {
                    b.Property<int>("idevento")
                        .HasColumnType("int");

                    b.Property<int>("idusuario")
                        .HasColumnType("int");

                    b.HasIndex("idevento");

                    b.HasIndex("idusuario");

                    b.ToTable("UsuariosEventos");
                });

            modelBuilder.Entity("Agenda.Models.UsuariosEventos", b =>
                {
                    b.HasOne("Agenda.Models.Evento", "Evento")
                        .WithMany()
                        .HasForeignKey("idevento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agenda.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("idusuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}