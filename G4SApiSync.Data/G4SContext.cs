using G4SApiSync.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace G4SApiSync.Data
{
    public class G4SContext : DbContext
    {
        public G4SContext(DbContextOptions<G4SContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        //Academy List
        public virtual DbSet<AcademySecurity> AcademySecurity { get; set; }
        public virtual DbSet<SyncResult> SyncResults { get; set; }

        //Students
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<EducationDetail> EducationDetails { get; set; }
        public virtual DbSet<StudentAttribute> StudentAttributes { get; set; }
        public virtual DbSet<StudentAttributeValue> StudentAttributeValues { get; set; }
        public virtual DbSet<AttributeType> AttributeTypes { get; set; }
        public virtual DbSet<AttributeValue> AttributeValues { get; set; }

        //Assessment
        public virtual DbSet<Marksheet> Marksheets { get; set; }
        public virtual DbSet<MarksheetGrade> MarksheetGrades { get; set; }
        public virtual DbSet<Markslot> Markslots { get; set; }
        public virtual DbSet<MarkslotMark> MarkslotMarks { get; set; }

        //Teaching
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }


        //Fluent API Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.HasDefaultSchema("g4s");

            //Students

            //modelBuilder.Entity<Student>()
            //    .HasOne<EducationDetail>(b => b.EducationDetail)
            //    .WithOne(c => c.Student)
            //    .HasForeignKey<EducationDetail>(s => s.StudentId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentAttribute>()
                .HasOne<EducationDetail>(b => b.EducationDetail)
                .WithMany(c => c.StudentAttributes)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<StudentAttributeValue>()
                .HasOne<StudentAttribute>(b => b.StudentAttribute)
                .WithMany(c => c.StudentAttributeValues)
                .HasForeignKey(s => s.StudentAttributeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttributeValue>()
                .HasOne<AttributeType>(b => b.AttributeType)
                .WithMany(c => c.AttributeValues)
                .HasForeignKey(s => s.AttributeTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AttributeValue>()
                .HasOne<Student>(b => b.Student)
                .WithMany(c => c.AttributeValues)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            //Teaching
            modelBuilder.Entity<Subject>()
                .HasOne<Department>(b => b.Department)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            //Assessment
            modelBuilder.Entity<Marksheet>()
                .HasOne<Subject>(b => b.Subject)
                .WithMany(c => c.Marksheets)
                .HasForeignKey(s => s.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Markslot>()
                .HasOne<Marksheet>(b => b.Marksheet)
                .WithMany(c => c.Markslots)
                .HasForeignKey(s => s.MarksheetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MarksheetGrade>()
                .HasOne<Marksheet>(b => b.Marksheet)
                .WithMany(c => c.MarksheetGrades)
                .HasForeignKey(s => s.MarksheetId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MarksheetGrade>()
                .HasOne<Student>(b => b.Student)
                .WithMany(c => c.MarksheetGrades)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MarkslotMark>()
                .HasOne<Student>(b => b.Student)
                .WithMany(c => c.MarkslotMarks)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MarkslotMark>()
                .HasOne<Markslot>(b => b.Markslot)
                .WithMany(c => c.MarkslotMarks)
                .HasForeignKey(s => s.MarkslotId)
                .OnDelete(DeleteBehavior.Cascade);

            //API Keys
            modelBuilder.Entity<AcademySecurity>()
                .ToTable("AcademySecurity", "sec");
        }
    }
}