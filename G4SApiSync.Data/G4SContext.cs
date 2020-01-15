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
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupStudent> GroupStudents { get; set; }

        //Attainment
        public virtual DbSet<PriorAttainment> PriorAttainment { get; set; }


        //Fluent API Configuration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            modelBuilder.HasDefaultSchema("g4s");

            //Students

            modelBuilder.Entity<EducationDetail>()
                .HasOne(b => b.Student)
                .WithOne(c => c.EducationDetail)
                .HasForeignKey<EducationDetail>(s => s.StudentId);

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

            //modelBuilder.Entity<AttributeValue>()
            //    .HasKey(pc => new { pc.StudentId, pc.AttributeTypeId });

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
            modelBuilder.Entity<GroupStudent>()
                .HasKey(pc => new { pc.StudentId, pc.GroupId });

            modelBuilder.Entity<Subject>()
                .HasOne<Department>(b => b.Department)
                .WithMany(c => c.Subjects)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Group>()
                .HasOne<Subject>(b => b.Subject)
                .WithMany(c => c.Groups)
                .HasForeignKey(s => s.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupStudent>()
                .HasOne<Group>(b => b.Group)
                .WithMany(c => c.GroupStudents)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupStudent>()
                .HasOne<Student>(b => b.Student)
                .WithMany(c => c.StudentGroups)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            //Assessment
            modelBuilder.Entity<MarksheetGrade>()
                .HasKey(pc => new { pc.StudentId, pc.MarksheetId });

            modelBuilder.Entity<MarkslotMark>()
                .HasKey(pc => new { pc.StudentId, pc.MarkslotId });

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

            //Attainment
            modelBuilder.Entity<PriorAttainment>()
                .HasKey(pc => new { pc.StudentId, pc.Code });

            modelBuilder.Entity<PriorAttainment>()
                .HasOne<Student>(b => b.Student)
                .WithMany(c => c.PriorAttainment)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);




            //API Keys
            modelBuilder.Entity<AcademySecurity>()
                .ToTable("AcademySecurity", "sec");
        }
    }
}