﻿// <auto-generated />
using EFCoreHeirarchyTPC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreHeirarchyTPC.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250315113511_Create")]
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

            modelBuilder.HasSequence("PersonSequence");

            modelBuilder.Entity("EFCoreHeirarchyTPC.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR [PersonSequence]");

                    SqlServerPropertyBuilderExtensions.UseSequence(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable((string)null);

                    b.UseTpcMappingStrategy();
                });

            modelBuilder.Entity("EFCoreHeirarchyTPC.Models.Student", b =>
                {
                    b.HasBaseType("EFCoreHeirarchyTPC.Models.Person");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("EFCoreHeirarchyTPC.Models.Teacher", b =>
                {
                    b.HasBaseType("EFCoreHeirarchyTPC.Models.Person");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Teachers", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
