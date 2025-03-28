﻿// <auto-generated />
using EFCoreHeirarchyTPT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreHeirarchyTPT.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCoreHeirarchyTPT.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("EFCoreHeirarchyTPT.Models.Student", b =>
                {
                    b.HasBaseType("EFCoreHeirarchyTPT.Models.Person");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("EFCoreHeirarchyTPT.Models.Teacher", b =>
                {
                    b.HasBaseType("EFCoreHeirarchyTPT.Models.Person");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Teachers", (string)null);
                });

            modelBuilder.Entity("EFCoreHeirarchyTPT.Models.Student", b =>
                {
                    b.HasOne("EFCoreHeirarchyTPT.Models.Person", null)
                        .WithOne()
                        .HasForeignKey("EFCoreHeirarchyTPT.Models.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EFCoreHeirarchyTPT.Models.Teacher", b =>
                {
                    b.HasOne("EFCoreHeirarchyTPT.Models.Person", null)
                        .WithOne()
                        .HasForeignKey("EFCoreHeirarchyTPT.Models.Teacher", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
