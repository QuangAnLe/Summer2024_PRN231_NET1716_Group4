﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MilkTeaBusinessObject.BusinessObject;

#nullable disable

namespace MilkTeaBusinessObject.Migrations
{
    [DbContext(typeof(MilkTeaDeliveryDBContext))]
    [Migration("20240514020545_v1")]
    partial class v1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Comment", b =>
                {
                    b.Property<int>("CommentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentID"), 1L, 1);

                    b.Property<DateTime>("CommentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("TeaID")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TeaID1")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CommentID");

                    b.HasIndex("TeaID1");

                    b.HasIndex("UserID");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.District", b =>
                {
                    b.Property<string>("DistrictID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DistrictName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WardName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DistrictID");

                    b.ToTable("District", (string)null);
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Material", b =>
                {
                    b.Property<int>("MaterialID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MaterialID"), 1L, 1);

                    b.Property<string>("MaterialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("TeaID")
                        .HasColumnType("int");

                    b.HasKey("MaterialID");

                    b.HasIndex("TeaID");

                    b.ToTable("Material", (string)null);
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderID"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShipAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TypeOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.HasIndex("UserID");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.OrderDetail", b =>
                {
                    b.Property<int>("TeaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeaID"), 1L, 1);

                    b.Property<string>("CostsIncurred")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TeaID1")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("TeaID");

                    b.HasIndex("OrderID");

                    b.HasIndex("TeaID1");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Role", b =>
                {
                    b.Property<string>("RoleID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleID");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Tea", b =>
                {
                    b.Property<int>("TeaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeaID"), 1L, 1);

                    b.Property<int>("Estimation")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("TeaDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeaName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeaID");

                    b.ToTable("Tea", (string)null);
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserID"), 1L, 1);

                    b.Property<string>("DistrictID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleID")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("UserAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.HasIndex("DistrictID");

                    b.HasIndex("RoleID");

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Comment", b =>
                {
                    b.HasOne("MilkTeaBusinessObject.BusinessObject.Tea", "Tea")
                        .WithMany("Comments")
                        .HasForeignKey("TeaID1")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MilkTeaBusinessObject.BusinessObject.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Tea");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Material", b =>
                {
                    b.HasOne("MilkTeaBusinessObject.BusinessObject.Tea", "Tea")
                        .WithMany("Materials")
                        .HasForeignKey("TeaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Tea");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Order", b =>
                {
                    b.HasOne("MilkTeaBusinessObject.BusinessObject.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.OrderDetail", b =>
                {
                    b.HasOne("MilkTeaBusinessObject.BusinessObject.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MilkTeaBusinessObject.BusinessObject.Tea", "Tea")
                        .WithMany("OrderDetails")
                        .HasForeignKey("TeaID1")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Tea");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.User", b =>
                {
                    b.HasOne("MilkTeaBusinessObject.BusinessObject.District", "District")
                        .WithMany("Users")
                        .HasForeignKey("DistrictID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MilkTeaBusinessObject.BusinessObject.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("District");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.District", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Tea", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Materials");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
