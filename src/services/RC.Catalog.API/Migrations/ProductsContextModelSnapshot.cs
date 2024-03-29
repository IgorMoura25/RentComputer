﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RC.Catalog.API.Data;

#nullable disable

namespace RC.Catalog.API.Migrations
{
    [DbContext(typeof(CatalogContext))]
    partial class ProductsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("RC.Catalog.API.Domain.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ProductId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<Guid>("UniversalId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductGuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(500)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(12,2)");

                    b.HasKey("Id", "UniversalId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("RC.Catalog.API.Domain.ProductImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("ProductImageId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<Guid>("UniversalId")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("ProductImageGuid");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<Guid>("ProductUniversalId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id", "UniversalId");

                    b.HasIndex("ProductId", "ProductUniversalId");

                    b.ToTable("ProductImages", (string)null);
                });

            modelBuilder.Entity("RC.Catalog.API.Domain.ProductImage", b =>
                {
                    b.HasOne("RC.Catalog.API.Domain.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId", "ProductUniversalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RC.Catalog.API.Domain.Product", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
