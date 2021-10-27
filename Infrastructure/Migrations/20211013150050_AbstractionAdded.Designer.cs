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
    [Migration("20211013150050_AbstractionAdded")]
    partial class AbstractionAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.IPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IPerson");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IPerson");
                });

            modelBuilder.Entity("Domain.PatientFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DischargeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IntakeDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("IntakePersonId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfTreatments")
                        .HasColumnType("int");

                    b.Property<int?>("PatientId")
                        .HasColumnType("int");

                    b.Property<int?>("PhysiotherapistId")
                        .HasColumnType("int");

                    b.Property<bool>("Student")
                        .HasColumnType("bit");

                    b.Property<string>("Treatment")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IntakePersonId");

                    b.HasIndex("PatientId");

                    b.HasIndex("PhysiotherapistId");

                    b.ToTable("PatientFiles");
                });

            modelBuilder.Entity("Domain.Remark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PatientFileId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("PatientFileId");

                    b.HasIndex("PersonId");

                    b.ToTable("Remarks");
                });

            modelBuilder.Entity("Domain.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Room")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specifics")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TherapistId")
                        .HasColumnType("int");

                    b.Property<DateTime>("TreatmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TherapistId");

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("Domain.Patient", b =>
                {
                    b.HasBaseType("Domain.IPerson");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdentificationNumber")
                        .HasColumnType("int");

                    b.Property<byte[]>("Img")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Physiotherapist")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("Domain.Physiotherapist", b =>
                {
                    b.HasBaseType("Domain.IPerson");

                    b.Property<int>("BIGNumber")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeNumber")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Physiotherapist_PhoneNumber");

                    b.HasDiscriminator().HasValue("Physiotherapist");
                });

            modelBuilder.Entity("Domain.Student", b =>
                {
                    b.HasBaseType("Domain.IPerson");

                    b.Property<int>("StudentNumber")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Domain.PatientFile", b =>
                {
                    b.HasOne("Domain.IPerson", "IntakePerson")
                        .WithMany()
                        .HasForeignKey("IntakePersonId");

                    b.HasOne("Domain.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.HasOne("Domain.Physiotherapist", "Physiotherapist")
                        .WithMany()
                        .HasForeignKey("PhysiotherapistId");

                    b.Navigation("IntakePerson");

                    b.Navigation("Patient");

                    b.Navigation("Physiotherapist");
                });

            modelBuilder.Entity("Domain.Remark", b =>
                {
                    b.HasOne("Domain.PatientFile", null)
                        .WithMany("Remarks")
                        .HasForeignKey("PatientFileId");

                    b.HasOne("Domain.IPerson", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Domain.Treatment", b =>
                {
                    b.HasOne("Domain.IPerson", "Therapist")
                        .WithMany()
                        .HasForeignKey("TherapistId");

                    b.Navigation("Therapist");
                });

            modelBuilder.Entity("Domain.PatientFile", b =>
                {
                    b.Navigation("Remarks");
                });
#pragma warning restore 612, 618
        }
    }
}
