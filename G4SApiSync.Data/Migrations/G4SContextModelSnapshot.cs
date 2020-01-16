﻿// <auto-generated />
using System;
using G4SApiSync.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace G4SApiSync.Data.Migrations
{
    [DbContext(typeof(G4SContext))]
    partial class G4SContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("CurrentAcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<int>("HighestYear")
                        .HasColumnType("int");

                    b.Property<int>("LowestYear")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("AcademyCode");

                    b.ToTable("AcademySecurity","sec");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.AttributeType", b =>
                {
                    b.Property<string>("AttributeTypeId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("AttributeGroup")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("AttributeName")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

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
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

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

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

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

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("AdmissionDate")
                        .HasColumnType("Date");

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("FormerUPN")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<int>("G4SStuId")
                        .HasColumnType("int");

                    b.Property<string>("House")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("LeavingDate")
                        .HasColumnType("Date");

                    b.Property<string>("NCYear")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("RegistrationGroup")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("UPN")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.HasKey("StudentId");

                    b.ToTable("EducationDetails");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.ExamResult", b =>
                {
                    b.Property<int>("ExamResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("ExamAcademicYear")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("ExamDate")
                        .HasColumnType("Date");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("KS123Literal")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("NCYear")
                        .HasColumnType("int");

                    b.Property<string>("QAN")
                        .HasColumnType("nvarchar(13)")
                        .HasMaxLength(13);

                    b.Property<string>("QualificationTitle")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ExamResultId");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("ExamResults");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Grade", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("GradeTypeId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<int>("NCYear")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("StudentId", "GradeTypeId", "SubjectId");

                    b.HasIndex("GradeTypeId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.GradeName", b =>
                {
                    b.Property<string>("GradeNameId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("GradeTypeId")
                        .HasColumnType("int");

                    b.Property<int>("NCYear")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<bool>("PreferredProgressGrade")
                        .HasColumnType("bit");

                    b.Property<bool>("PreferredTargetGrade")
                        .HasColumnType("bit");

                    b.Property<string>("ShortName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("GradeNameId");

                    b.HasIndex("GradeTypeId");

                    b.ToTable("GradeNames");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.GradeType", b =>
                {
                    b.Property<int>("GradeTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GradeTypeId");

                    b.ToTable("GradeTypes");

                    b.HasData(
                        new
                        {
                            GradeTypeId = 1,
                            Name = "External target"
                        },
                        new
                        {
                            GradeTypeId = 2,
                            Name = "Teacher target"
                        },
                        new
                        {
                            GradeTypeId = 3,
                            Name = "Combined target"
                        },
                        new
                        {
                            GradeTypeId = 4,
                            Name = "Current"
                        },
                        new
                        {
                            GradeTypeId = 5,
                            Name = "Project"
                        },
                        new
                        {
                            GradeTypeId = 6,
                            Name = "Actual"
                        },
                        new
                        {
                            GradeTypeId = 7,
                            Name = "Honest"
                        },
                        new
                        {
                            GradeTypeId = 8,
                            Name = "Aspirational"
                        },
                        new
                        {
                            GradeTypeId = 9,
                            Name = "Additional target"
                        },
                        new
                        {
                            GradeTypeId = 10,
                            Name = "Baseline grade"
                        });
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Group", b =>
                {
                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("GroupId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.GroupStudent", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("GroupId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("StudentId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupStudents");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Marksheet", b =>
                {
                    b.Property<string>("MarksheetId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

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
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MarksheetId")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId", "MarksheetId");

                    b.HasIndex("MarksheetId");

                    b.ToTable("MarksheetGrades");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Markslot", b =>
                {
                    b.Property<string>("MarkslotId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MarksheetId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.HasKey("MarkslotId");

                    b.HasIndex("MarksheetId");

                    b.ToTable("Markslots");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.MarkslotMark", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("MarkslotId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Alias")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<float?>("Mark")
                        .HasColumnType("real");

                    b.HasKey("StudentId", "MarkslotId");

                    b.HasIndex("MarkslotId");

                    b.ToTable("MarkslotMarks");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.PriorAttainment", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ValueAcademicYear")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("ValueDate")
                        .HasColumnType("Date");

                    b.HasKey("StudentId", "Code");

                    b.ToTable("PriorAttainment");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

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
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("G4SStuId")
                        .HasColumnType("int");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("StudentId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("StudentAttributeId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAttributes");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.StudentAttributeValue", b =>
                {
                    b.Property<string>("StudentAttributeId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("AcademicYear")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("Date");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("StudentAttributeId");

                    b.ToTable("StudentAttributeValues");
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Subject", b =>
                {
                    b.Property<string>("SubjectId")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Academy")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

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

                    b.Property<string>("AcademyCode")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("DataSet")
                        .HasColumnType("nvarchar(4)")
                        .HasMaxLength(4);

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
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.EducationDetail", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithOne("EducationDetail")
                        .HasForeignKey("G4SApiSync.Data.Entities.EducationDetail", "StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.ExamResult", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithMany("ExamResults")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("G4SApiSync.Data.Entities.Subject", "Subject")
                        .WithMany("ExamResults")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Grade", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.GradeType", "GradeType")
                        .WithMany("Grades")
                        .HasForeignKey("GradeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G4SApiSync.Data.Entities.Subject", "Subject")
                        .WithMany("Grades")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.GradeName", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.GradeType", "GradeType")
                        .WithMany("GradeNames")
                        .HasForeignKey("GradeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Group", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Subject", "Subject")
                        .WithMany("Groups")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.GroupStudent", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Group", "Group")
                        .WithMany("GroupStudents")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithMany("StudentGroups")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Marksheet", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Subject", "Subject")
                        .WithMany("Marksheets")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.MarksheetGrade", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Marksheet", "Marksheet")
                        .WithMany("MarksheetGrades")
                        .HasForeignKey("MarksheetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithMany("MarksheetGrades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.Markslot", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Marksheet", "Marksheet")
                        .WithMany("Markslots")
                        .HasForeignKey("MarksheetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.MarkslotMark", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Markslot", "Markslot")
                        .WithMany("MarkslotMarks")
                        .HasForeignKey("MarkslotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithMany("MarkslotMarks")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("G4SApiSync.Data.Entities.PriorAttainment", b =>
                {
                    b.HasOne("G4SApiSync.Data.Entities.Student", "Student")
                        .WithMany("PriorAttainment")
                        .HasForeignKey("StudentId")
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
