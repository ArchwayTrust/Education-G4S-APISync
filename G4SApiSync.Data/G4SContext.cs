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
            optionsBuilder.UseSqlServer("Server=core-sql-svc;Database=G4S;Trusted_Connection=True;");
            //optionsBuilder.UseSqlServer("Server=(localdb)\MSSQLLocalDB;Database=G4S;Trusted_Connection=True;");
        }

        //Academy List
        public virtual DbSet<AcademySecurity> AcademySecurity { get; set; }

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

            //Teaching
            modelBuilder.Entity<Subject>()
                .HasOne<Department>(b => b.Department)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            //API Keys
            modelBuilder.Entity<AcademySecurity>()
                .ToTable("AcademySecurity", "sec");
        }
    }
}