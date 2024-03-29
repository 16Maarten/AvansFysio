﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(DbFysioContext))]
    [Migration("20211025191452_changes")]
    partial class changes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Patient", b =>
                {
                    b.Property<int>("PatientNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdentificationNumber")
                        .HasColumnType("int");

                    b.Property<byte[]>("Img")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Student")
                        .HasColumnType("bit");

                    b.HasKey("PatientNumber");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Domain.PatientFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("DescriptionDiagnosticCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiagnosticCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("DischargeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IntakeDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsStudent")
                        .HasColumnType("bit");

                    b.Property<int?>("PatientNumber")
                        .HasColumnType("int");

                    b.Property<int?>("PhysiotherapistId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("TreatmentPlanId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientNumber");

                    b.HasIndex("PhysiotherapistId");

                    b.HasIndex("StudentId");

                    b.HasIndex("TreatmentPlanId");

                    b.ToTable("PatientFiles");
                });

            modelBuilder.Entity("Domain.Physiotherapist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BIGNumber")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdentificationNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Physiotherapists");
                });

            modelBuilder.Entity("Domain.Remark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatientFileId")
                        .HasColumnType("int");

                    b.Property<int?>("PhysiotherapistId")
                        .HasColumnType("int");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.Property<int?>("studentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientFileId");

                    b.HasIndex("PhysiotherapistId");

                    b.HasIndex("studentId");

                    b.ToTable("Remarks");
                });

            modelBuilder.Entity("Domain.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdentificationNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domain.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatientFileId")
                        .HasColumnType("int");

                    b.Property<int?>("PhysiotherapistId")
                        .HasColumnType("int");

                    b.Property<string>("Room")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specifics")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TreatmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("studentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientFileId");

                    b.HasIndex("PhysiotherapistId");

                    b.HasIndex("studentId");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("Domain.TreatmentPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DescriptionDiagnosticCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DiagnosticCode")
                        .HasColumnType("int");

                    b.Property<int>("DurationTreatment")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfTreatmentsPerWeek")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TreatmentPlan");
                });

            modelBuilder.Entity("Domain.PatientFile", b =>
                {
                    b.HasOne("Domain.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientNumber");

                    b.HasOne("Domain.Physiotherapist", "Physiotherapist")
                        .WithMany()
                        .HasForeignKey("PhysiotherapistId");

                    b.HasOne("Domain.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");

                    b.HasOne("Domain.TreatmentPlan", "TreatmentPlan")
                        .WithMany()
                        .HasForeignKey("TreatmentPlanId");

                    b.Navigation("Patient");

                    b.Navigation("Physiotherapist");

                    b.Navigation("Student");

                    b.Navigation("TreatmentPlan");
                });

            modelBuilder.Entity("Domain.Remark", b =>
                {
                    b.HasOne("Domain.PatientFile", null)
                        .WithMany("Remarks")
                        .HasForeignKey("PatientFileId");

                    b.HasOne("Domain.Physiotherapist", "Physiotherapist")
                        .WithMany()
                        .HasForeignKey("PhysiotherapistId");

                    b.HasOne("Domain.Student", "student")
                        .WithMany()
                        .HasForeignKey("studentId");

                    b.Navigation("Physiotherapist");

                    b.Navigation("student");
                });

            modelBuilder.Entity("Domain.Treatment", b =>
                {
                    b.HasOne("Domain.PatientFile", null)
                        .WithMany("Treatments")
                        .HasForeignKey("PatientFileId");

                    b.HasOne("Domain.Physiotherapist", "Physiotherapist")
                        .WithMany()
                        .HasForeignKey("PhysiotherapistId");

                    b.HasOne("Domain.Student", "student")
                        .WithMany()
                        .HasForeignKey("studentId");

                    b.Navigation("Physiotherapist");

                    b.Navigation("student");
                });

            modelBuilder.Entity("Domain.PatientFile", b =>
                {
                    b.Navigation("Remarks");

                    b.Navigation("Treatments");
                });
#pragma warning restore 612, 618
        }
    }
}
