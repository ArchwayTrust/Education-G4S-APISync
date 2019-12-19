﻿// <auto-generated />
using System;
using G4SApiSync.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace G4SApiSync.Data.Migrations
{
    [DbContext(typeof(G4SContext))]
    [Migration("20191219140954_Migration3")]
    partial class Migration3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("g4s")
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("G4SApiSync.Data.Entities.AcademySecurity", b =>
                {
                    b.Property<string>("AcademyCode")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("APIKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentAcademicYear")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AcademyCode");

                    b.ToTable("AcademySecurity","sec");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.AttributeType", b =>
                {
                    b.Property<string>("AttributeTypeId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("AttributeGroup")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("AttributeName")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("G4SAttributeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("bit");

                    b.HasKey("AttributeTypeId");

                    b.ToTable("AttributeTypes");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.AttributeValue", b =>
                {
                    b.Property<int>("AttributeValueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("AttributeTypeId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("AttributeValueId");

                    b.HasIndex("AttributeTypeId");

                    b.HasIndex("StudentId");

                    b.ToTable("AttributeValues");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Department", b =>
                {
                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("G4SDepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("DepartmentId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.EducationDetail", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("AdmissionDate")
                        .HasColumnType("Date");

                    b.Property<string>("FormerUPN")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<int>("G4SStuId")
                        .HasColumnType("int");

                    b.Property<string>("House")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("LeavingDate")
                        .HasColumnType("Date");

                    b.Property<string>("NCYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("RegistrationGroup")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("UPN")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.HasKey("StudentId");

                    b.ToTable("EducationDetails");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Marksheet", b =>
                {
                    b.Property<string>("MarksheetId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("MarksheetId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Marksheets");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.MarksheetGrade", b =>
                {
                    b.Property<int>("MarksheetGradeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Mark")
                        .HasColumnType("real");

                    b.Property<int>("MarksheetId")
                        .HasColumnType("int");

                    b.Property<string>("MarksheetId1")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("MarksheetGradeId");

                    b.HasIndex("MarksheetId1");

                    b.HasIndex("StudentId");

                    b.ToTable("MarksheetGrades");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Markslot", b =>
                {
                    b.Property<string>("MarkslotId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("MarksheetId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("MarkslotId");

                    b.HasIndex("MarksheetId");

                    b.ToTable("Markslots");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.MarkslotMark", b =>
                {
                    b.Property<int>("MarkslotMarkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<float>("Mark")
                        .HasColumnType("real");

                    b.Property<string>("MarkslotId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MockslotId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("MarkslotMarkId");

                    b.HasIndex("MarkslotId");

                    b.HasIndex("StudentId");

                    b.ToTable("MarkslotMarks");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("Date");

                    b.Property<int>("G4SStuId")
                        .HasColumnType("int");

                    b.Property<string>("LegalFirstName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("LegalLastName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("MiddleNames")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("PreferredFirstName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("PreferredLastName")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Sex")
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.StudentAttribute", b =>
                {
                    b.Property<string>("StudentAttributeId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("AttributeId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("G4SStuId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("StudentAttributeId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAttributes");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.StudentAttributeValue", b =>
                {
                    b.Property<int>("StudentAttributeValueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("StudentAttributeId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.HasKey("StudentAttributeValueId");

                    b.HasIndex("StudentAttributeId");

                    b.ToTable("StudentAttributeValues");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Subject", b =>
                {
                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("DepartmentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("G4SSubjectId")
                        .HasColumnType("int");

                    b.Property<bool>("IncludeInStats")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("QAN")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("QualificationSchemeName")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("QualificationTitle")
                        .HasColumnType("nvarchar(300)")
                        .HasMaxLength(300);

                    b.Property<string>("YearGroup")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("SubjectId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.SyncResult", b =>
                {
                    b.Property<int>("SyncResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("AcademyCode")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("EndPoint")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Exception")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("LoggedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Result")
                        .HasColumnType("bit");

                    b.Property<int?>("YearGroup")
                        .HasColumnType("int");

                    b.HasKey("SyncResultId");

                    b.ToTable("SyncResults");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.AttributeValue", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.AttributeType", "AttributeType")
                        .WithMany("AttributeValues")
                        .HasForeignKey("AttributeTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithMany("AttributeValues")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Marksheet", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Subject", "Subject")
                        .WithMany("Marksheets")
                        .HasForeignKey("SubjectId");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.MarksheetGrade", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Marksheet", "Marksheet")
                        .WithMany("MarksheetGrades")
                        .HasForeignKey("MarksheetId1");

                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithMany("MarksheetGrades")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Markslot", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Marksheet", "Marksheet")
                        .WithMany("Markslots")
                        .HasForeignKey("MarksheetId");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.MarkslotMark", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Markslot", "Markslot")
                        .WithMany("MarkslotMarks")
                        .HasForeignKey("MarkslotId");

                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithMany("MarkslotMarks")
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Student", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.EducationDetail", "EducationDetail")
                        .WithOne("Student")
                        .HasForeignKey("G4SApiSync.Data.Entities.Student", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.StudentAttribute", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.EducationDetail", "EducationDetail")
                        .WithMany("StudentAttributes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.StudentAttributeValue", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.StudentAttribute", "StudentAttribute")
                        .WithMany("StudentAttributeValues")
                        .HasForeignKey("StudentAttributeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Subject", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Department", "Department")
                        .WithMany("Subjects")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
