﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NewBackend2.Repository;

#nullable disable

namespace NewBackend2.Migrations
{
    [DbContext(typeof(ProjectDatabaseConfiguration))]
    [Migration("20230425144249_CookieMigration")]
    partial class CookieMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DiagnosticEntitySymptomEntity", b =>
                {
                    b.Property<int>("DiagnosticsDiagnosticId")
                        .HasColumnType("int");

                    b.Property<string>("SymptomName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("DiagnosticsDiagnosticId", "SymptomName");

                    b.HasIndex("SymptomName");

                    b.ToTable("DiagnosticEntitySymptomEntity");
                });

            modelBuilder.Entity("NewBackend2.Model.AppointmentEntity", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"), 1L, 1);

                    b.Property<DateTime>("AppointmentDate")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("UserId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("NewBackend2.Model.CollegeEntity", b =>
                {
                    b.Property<int>("CollegeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CollegeId"), 1L, 1);

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CollegeId");

                    b.ToTable("College");
                });

            modelBuilder.Entity("NewBackend2.Model.CookiesEntity", b =>
                {
                    b.Property<int>("CookieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CookieId"), 1L, 1);

                    b.Property<DateTime>("DateTime")
                        .HasMaxLength(100)
                        .HasColumnType("datetime2");

                    b.Property<string>("Identifier")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CookieId");

                    b.ToTable("Cookies");
                });

            modelBuilder.Entity("NewBackend2.Model.DegreeEntity", b =>
                {
                    b.Property<int>("DegreeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DegreeId"), 1L, 1);

                    b.Property<int>("CollegeId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<int>("DoctorId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<DateTime>("EndYear")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartYear")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudyField")
                        .HasColumnType("int");

                    b.Property<int>("StudyProgram")
                        .HasColumnType("int");

                    b.HasKey("DegreeId");

                    b.HasIndex("CollegeId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Degree");
                });

            modelBuilder.Entity("NewBackend2.Model.DiagnosticEntity", b =>
                {
                    b.Property<int>("DiagnosticId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DiagnosticId"), 1L, 1);

                    b.Property<string>("DiseaseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DoctorSpecialization")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DoctorTitle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SymptomList")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.HasKey("DiagnosticId");

                    b.HasIndex("DiseaseName");

                    b.HasIndex("UserId");

                    b.ToTable("Diagnostic");
                });

            modelBuilder.Entity("NewBackend2.Model.DiseaseEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Disease");
                });

            modelBuilder.Entity("NewBackend2.Model.DoctorEntity", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<float>("Rating")
                        .HasMaxLength(10)
                        .HasColumnType("real");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("NewBackend2.Model.EmailEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("NewBackend2.Model.EmploymentEntity", b =>
                {
                    b.Property<int>("EmploymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmploymentId"), 1L, 1);

                    b.Property<string>("ConsultPrice")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentPosition")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasMaxLength(100)
                        .HasColumnType("time");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<TimeSpan>("StartTime")
                        .HasMaxLength(100)
                        .HasColumnType("time");

                    b.Property<int>("WeekDay")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.HasKey("EmploymentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("HospitalName");

                    b.ToTable("Employment");
                });

            modelBuilder.Entity("NewBackend2.Model.EngineerEntity", b =>
                {
                    b.Property<int>("EngineerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EngineerId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Experience")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.HasKey("EngineerId");

                    b.ToTable("Engineer");
                });

            modelBuilder.Entity("NewBackend2.Model.HospitalEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Name");

                    b.ToTable("Hospital");
                });

            modelBuilder.Entity("NewBackend2.Model.ReviewEntity", b =>
                {
                    b.Property<int>("ReviewMappingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewMappingId"), 1L, 1);

                    b.Property<int>("DoctorId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.HasKey("ReviewMappingId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("UserId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("NewBackend2.Model.SubscriptionEntity", b =>
                {
                    b.Property<int>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubscriptionId"), 1L, 1);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("NewBackend2.Model.SymptomEntity", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Symptom");
                });

            modelBuilder.Entity("NewBackend2.Model.UserEntity", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("PhoneNumber")
                        .HasMaxLength(10)
                        .HasColumnType("int");

                    b.Property<bool>("isEmailConfirmed")
                        .HasMaxLength(100)
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("DiagnosticEntitySymptomEntity", b =>
                {
                    b.HasOne("NewBackend2.Model.DiagnosticEntity", null)
                        .WithMany()
                        .HasForeignKey("DiagnosticsDiagnosticId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewBackend2.Model.SymptomEntity", null)
                        .WithMany()
                        .HasForeignKey("SymptomName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NewBackend2.Model.AppointmentEntity", b =>
                {
                    b.HasOne("NewBackend2.Model.DoctorEntity", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewBackend2.Model.UserEntity", "User")
                        .WithMany("Appointments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NewBackend2.Model.DegreeEntity", b =>
                {
                    b.HasOne("NewBackend2.Model.CollegeEntity", "College")
                        .WithMany("DoctorColleges")
                        .HasForeignKey("CollegeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewBackend2.Model.DoctorEntity", "Doctor")
                        .WithMany("Degrees")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("College");

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("NewBackend2.Model.DiagnosticEntity", b =>
                {
                    b.HasOne("NewBackend2.Model.DiseaseEntity", "Disease")
                        .WithMany("Diagnosis")
                        .HasForeignKey("DiseaseName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewBackend2.Model.UserEntity", "User")
                        .WithMany("Diagnostics")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Disease");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NewBackend2.Model.EmploymentEntity", b =>
                {
                    b.HasOne("NewBackend2.Model.DoctorEntity", "Doctor")
                        .WithMany("Employments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewBackend2.Model.HospitalEntity", "Hospital")
                        .WithMany("Employments")
                        .HasForeignKey("HospitalName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Hospital");
                });

            modelBuilder.Entity("NewBackend2.Model.ReviewEntity", b =>
                {
                    b.HasOne("NewBackend2.Model.DoctorEntity", "Doctor")
                        .WithMany("Reviews")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NewBackend2.Model.UserEntity", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("User");
                });

            modelBuilder.Entity("NewBackend2.Model.SubscriptionEntity", b =>
                {
                    b.HasOne("NewBackend2.Model.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NewBackend2.Model.CollegeEntity", b =>
                {
                    b.Navigation("DoctorColleges");
                });

            modelBuilder.Entity("NewBackend2.Model.DiseaseEntity", b =>
                {
                    b.Navigation("Diagnosis");
                });

            modelBuilder.Entity("NewBackend2.Model.DoctorEntity", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Degrees");

                    b.Navigation("Employments");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("NewBackend2.Model.HospitalEntity", b =>
                {
                    b.Navigation("Employments");
                });

            modelBuilder.Entity("NewBackend2.Model.UserEntity", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Diagnostics");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
