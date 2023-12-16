﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PetCareAPI.Data;

#nullable disable

namespace PetCareAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PetCareAPI.Models.Activity", b =>
                {
                    b.Property<int>("ActivityId")
                        .HasColumnType("int");

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.HasKey("ActivityId");

                    b.HasIndex("PetId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("PetCareAPI.Models.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<string>("FoodName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.HasKey("FoodId");

                    b.HasIndex("PetId");

                    b.ToTable("Food");
                });

            modelBuilder.Entity("PetCareAPI.Models.HealthStatus", b =>
                {
                    b.Property<int>("HealthStatusId")
                        .HasColumnType("int");

                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HealthStatusId");

                    b.ToTable("HealthStatus");
                });

            modelBuilder.Entity("PetCareAPI.Models.Pet", b =>
                {
                    b.Property<int>("PetId")
                        .HasColumnType("int");

                    b.Property<int>("HealthStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PetId");

                    b.HasIndex("HealthStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Pet");
                });

            modelBuilder.Entity("PetCareAPI.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PetCareAPI.Models.Activity", b =>
                {
                    b.HasOne("PetCareAPI.Models.Pet", "Pet")
                        .WithMany("Activities")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("PetCareAPI.Models.Food", b =>
                {
                    b.HasOne("PetCareAPI.Models.Pet", "Pet")
                        .WithMany("Foods")
                        .HasForeignKey("PetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pet");
                });

            modelBuilder.Entity("PetCareAPI.Models.Pet", b =>
                {
                    b.HasOne("PetCareAPI.Models.HealthStatus", "HealthStatus")
                        .WithMany()
                        .HasForeignKey("HealthStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PetCareAPI.Models.User", "User")
                        .WithMany("Pets")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HealthStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PetCareAPI.Models.Pet", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("Foods");
                });

            modelBuilder.Entity("PetCareAPI.Models.User", b =>
                {
                    b.Navigation("Pets");
                });
#pragma warning restore 612, 618
        }
    }
}
