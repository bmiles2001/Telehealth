﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Telehealth.UI.Infrastructure;

#nullable disable

namespace Telehealth.UI.Infrastructure.Migrations.CreateDatabase
{
    [DbContext(typeof(TelehealthDbContext))]
    partial class TelehealthDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Telehealth.UI.Features.Practices.Appointments.Appointment+Details", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTimeOffset>("AppointmentTime")
                        .HasPrecision(0)
                        .HasColumnType("datetimeoffset(0)");

                    b.Property<string>("AppointmentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("Duration")
                        .HasPrecision(0)
                        .HasColumnType("time(0)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("int");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("AppointmentId"));

                    b.HasIndex("LocationId");

                    b.HasIndex("PatientId");

                    b.HasIndex("ProviderId");

                    b.ToTable("Appointment", (string)null);
                });

            modelBuilder.Entity("Telehealth.UI.Features.Practices.Locations.Location+Details", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PracticeId")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.ComplexProperty<Dictionary<string, object>>("OperatingHours", "Telehealth.UI.Features.Practices.Locations.Location+Details.OperatingHours#OperatingHours", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<TimeSpan>("ClosingTime")
                                .HasPrecision(0)
                                .HasColumnType("time(0)")
                                .HasColumnName("ClosingTime");

                            b1.Property<TimeSpan>("OpeningTime")
                                .HasPrecision(0)
                                .HasColumnType("time(0)")
                                .HasColumnName("OpeningTime");
                        });

                    b.HasKey("LocationId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("LocationId"));

                    b.HasIndex("PracticeId");

                    b.ToTable("Location", (string)null);
                });

            modelBuilder.Entity("Telehealth.UI.Features.Practices.Practice+Details", b =>
                {
                    b.Property<int>("PracticeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PracticeId"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVirtual")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PracticeId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("PracticeId"));

                    b.ToTable("Practice", (string)null);
                });

            modelBuilder.Entity("Telehealth.UI.Features.Practices.Providers.Provider+Details", b =>
                {
                    b.Property<int>("ProviderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProviderId"));

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<decimal>("Rate")
                        .HasPrecision(18)
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("ProviderId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("ProviderId"));

                    b.HasIndex("ProfileId");

                    b.ToTable("Provider", (string)null);
                });

            modelBuilder.Entity("Telehealth.UI.Features.Profiles.Profile+Details", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfileId"));

                    b.Property<DateOnly>("DateOfBirth")
                        .HasPrecision(0)
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(75)
                        .HasColumnType("nvarchar(75)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("ProfileType")
                        .HasColumnType("int");

                    b.HasKey("ProfileId");

                    SqlServerKeyBuilderExtensions.IsClustered(b.HasKey("ProfileId"));

                    b.HasIndex("ProfileType");

                    b.ToTable("Profile", (string)null);
                });

            modelBuilder.Entity("Telehealth.UI.Features.Practices.Appointments.Appointment+Details", b =>
                {
                    b.HasOne("Telehealth.UI.Features.Practices.Locations.Location+Details", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Telehealth.UI.Features.Profiles.Profile+Details", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Telehealth.UI.Features.Practices.Providers.Provider+Details", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Location");

                    b.Navigation("Patient");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Telehealth.UI.Features.Practices.Locations.Location+Details", b =>
                {
                    b.HasOne("Telehealth.UI.Features.Practices.Practice+Details", "Practice")
                        .WithMany("Locations")
                        .HasForeignKey("PracticeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Practice");
                });

            modelBuilder.Entity("Telehealth.UI.Features.Practices.Providers.Provider+Details", b =>
                {
                    b.HasOne("Telehealth.UI.Features.Profiles.Profile+Details", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Telehealth.UI.Features.Practices.Practice+Details", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
