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
    [Migration("20240521015436_v4")]
    partial class v4
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

                    b.Property<int>("TeaID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("CommentID");

                    b.HasIndex("TeaID");

                    b.HasIndex("UserID");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.DetailsMaterial", b =>
                {
                    b.Property<int>("DetailsMaterialID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DetailsMaterialID"), 1L, 1);

                    b.Property<string>("DetailMaterialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaterialID")
                        .HasColumnType("int");

                    b.Property<int>("Quanity")
                        .HasColumnType("int");

                    b.Property<int>("TeaID")
                        .HasColumnType("int");

                    b.HasKey("DetailsMaterialID");

                    b.HasIndex("MaterialID");

                    b.HasIndex("TeaID");

                    b.ToTable("DetailsMaterial", (string)null);
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

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("MaterialID");

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
                    b.Property<int>("OrderDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailID"), 1L, 1);

                    b.Property<string>("CostsIncurred")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TeaID")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("OrderDetailID");

                    b.HasIndex("OrderID");

                    b.HasIndex("TeaID");

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

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.TaskUser", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"), 1L, 1);

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("WorkDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaskId");

                    b.HasIndex("OrderID");

                    b.HasIndex("UserID");

                    b.ToTable("TaskUser", (string)null);
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

                    b.Property<double>("Price")
                        .HasColumnType("float");

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
                        .HasForeignKey("TeaID")
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

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.DetailsMaterial", b =>
                {
                    b.HasOne("MilkTeaBusinessObject.BusinessObject.Material", "Material")
                        .WithMany("DetailsMaterials")
                        .HasForeignKey("MaterialID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MilkTeaBusinessObject.BusinessObject.Tea", "Tea")
                        .WithMany("DetailsMaterials")
                        .HasForeignKey("TeaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Material");

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
                        .HasForeignKey("TeaID")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Tea");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.TaskUser", b =>
                {
                    b.HasOne("MilkTeaBusinessObject.BusinessObject.Order", "Order")
                        .WithMany("TaskUsers")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MilkTeaBusinessObject.BusinessObject.User", "User")
                        .WithMany("TaskUsers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("User");
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

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Material", b =>
                {
                    b.Navigation("DetailsMaterials");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Order", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("TaskUsers");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.Tea", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("DetailsMaterials");

                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("MilkTeaBusinessObject.BusinessObject.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Orders");

                    b.Navigation("TaskUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
