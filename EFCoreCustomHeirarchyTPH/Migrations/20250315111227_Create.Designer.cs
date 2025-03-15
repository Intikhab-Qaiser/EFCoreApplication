﻿// <auto-generated />
using EFCoreHeirarchyTPH.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreHeirarchyTPH.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250315111227_Create")]
    partial class Create
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCoreHeirarchyTPH.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasDiscriminator<string>("PersonType").HasValue("Person");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("EFCoreHeirarchyTPH.Models.Student", b =>
                {
                    b.HasBaseType("EFCoreHeirarchyTPH.Models.Person");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("EFCoreHeirarchyTPH.Models.Teacher", b =>
                {
                    b.HasBaseType("EFCoreHeirarchyTPH.Models.Person");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Teacher");
                });
#pragma warning restore 612, 618
        }
    }
}
