﻿// <auto-generated />
using JwtDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JwtWebApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200210220640_source")]
    partial class source
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1");

            modelBuilder.Entity("JwtModels.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Done")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Todo");
                });

            modelBuilder.Entity("JwtModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}