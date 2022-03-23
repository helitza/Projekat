﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Projekat.Migrations
{
    [DbContext(typeof(KompanijaContext))]
    partial class KompanijaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.Akreditacija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("OdrzavanjeId")
                        .HasColumnType("int");

                    b.Property<int?>("korisnikId")
                        .HasColumnType("int");

                    b.Property<int?>("sedisteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OdrzavanjeId");

                    b.HasIndex("korisnikId");

                    b.HasIndex("sedisteId");

                    b.ToTable("Akreditacije");
                });

            modelBuilder.Entity("Models.Kompanija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Kompanije");
                });

            modelBuilder.Entity("Models.KompanijaTribina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("KompanijaId")
                        .HasColumnType("int");

                    b.Property<int?>("TribinaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KompanijaId");

                    b.HasIndex("TribinaId");

                    b.ToTable("KopmanijeTribine");
                });

            modelBuilder.Entity("Models.Korisnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("admin")
                        .HasColumnType("bit");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sifra")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("Models.Mesto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BrRedova")
                        .HasColumnType("int");

                    b.Property<int>("BrSedistaPoRedu")
                        .HasColumnType("int");

                    b.Property<int?>("KompanijaId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("KompanijaId");

                    b.ToTable("Mesta");
                });

            modelBuilder.Entity("Models.Odrzavanje", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("KompanijaId")
                        .HasColumnType("int");

                    b.Property<int?>("TribinaId")
                        .HasColumnType("int");

                    b.Property<int?>("mestoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("vreme")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("KompanijaId");

                    b.HasIndex("TribinaId");

                    b.HasIndex("mestoId");

                    b.ToTable("Odrzavanja");
                });

            modelBuilder.Entity("Models.Sediste", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BrReda")
                        .HasColumnType("int");

                    b.Property<int>("BrSedistaURedu")
                        .HasColumnType("int");

                    b.Property<int?>("mestoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("mestoId");

                    b.ToTable("Sedista");
                });

            modelBuilder.Entity("Models.Tribina", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Kotizacija")
                        .HasColumnType("int");

                    b.Property<DateTime>("datumKraja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("datumPocetka")
                        .HasColumnType("datetime2");

                    b.Property<string>("naziv")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("sektor")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Tribine");
                });

            modelBuilder.Entity("Models.Akreditacija", b =>
                {
                    b.HasOne("Models.Odrzavanje", "Odrzavanje")
                        .WithMany("Akreditacije")
                        .HasForeignKey("OdrzavanjeId");

                    b.HasOne("Models.Korisnik", "korisnik")
                        .WithMany()
                        .HasForeignKey("korisnikId");

                    b.HasOne("Models.Sediste", "sediste")
                        .WithMany()
                        .HasForeignKey("sedisteId");

                    b.Navigation("korisnik");

                    b.Navigation("Odrzavanje");

                    b.Navigation("sediste");
                });

            modelBuilder.Entity("Models.KompanijaTribina", b =>
                {
                    b.HasOne("Models.Kompanija", "Kompanija")
                        .WithMany("Tribine")
                        .HasForeignKey("KompanijaId");

                    b.HasOne("Models.Tribina", "Tribina")
                        .WithMany("Kompanije")
                        .HasForeignKey("TribinaId");

                    b.Navigation("Kompanija");

                    b.Navigation("Tribina");
                });

            modelBuilder.Entity("Models.Mesto", b =>
                {
                    b.HasOne("Models.Kompanija", null)
                        .WithMany("Mesta")
                        .HasForeignKey("KompanijaId");
                });

            modelBuilder.Entity("Models.Odrzavanje", b =>
                {
                    b.HasOne("Models.Kompanija", "Kompanija")
                        .WithMany("Odrzavanje")
                        .HasForeignKey("KompanijaId");

                    b.HasOne("Models.Tribina", "Tribina")
                        .WithMany()
                        .HasForeignKey("TribinaId");

                    b.HasOne("Models.Mesto", "mesto")
                        .WithMany()
                        .HasForeignKey("mestoId");

                    b.Navigation("Kompanija");

                    b.Navigation("mesto");

                    b.Navigation("Tribina");
                });

            modelBuilder.Entity("Models.Sediste", b =>
                {
                    b.HasOne("Models.Mesto", "mesto")
                        .WithMany()
                        .HasForeignKey("mestoId");

                    b.Navigation("mesto");
                });

            modelBuilder.Entity("Models.Kompanija", b =>
                {
                    b.Navigation("Mesta");

                    b.Navigation("Odrzavanje");

                    b.Navigation("Tribine");
                });

            modelBuilder.Entity("Models.Odrzavanje", b =>
                {
                    b.Navigation("Akreditacije");
                });

            modelBuilder.Entity("Models.Tribina", b =>
                {
                    b.Navigation("Kompanije");
                });
#pragma warning restore 612, 618
        }
    }
}