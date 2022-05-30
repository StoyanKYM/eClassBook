using eClassBook.Models;
using eClassBook.Models.Enumerations;
using eClassBook.Models.SchoolUserEntities;
using Microsoft.EntityFrameworkCore;

namespace eClassBook.Data
{
    public class eClassBookDbContext : Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<ApplicationUser>
    {
        public eClassBookDbContext(DbContextOptions<eClassBookDbContext> options) : base(options)
        {
        }

        public DbSet<SchoolUser> SchoolUsers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Principal> Principals { get; set; }

        public DbSet<School> Schools { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Absence> Absences { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassToSubject> ClassesToSubjects { get; set; }

        public DbSet<StudentToGrade> StudentsToGrades { get; set; }

        public DbSet<TeacherToSubject> TeachersToSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SchoolUser>()
                .Property(s => s.Role)
                .HasConversion<string>();

            builder.Entity<SchoolUser>()
                .HasDiscriminator(o => o.Role)
                .HasValue<SchoolUser>(RoleTypes.NotUser)
                .HasValue<Student>(RoleTypes.Student)
                .HasValue<Teacher>(RoleTypes.Teacher)
                .HasValue<Principal>(RoleTypes.Principal)
                .HasValue<Parent>(RoleTypes.Parent)
                .HasValue<SchoolAdmin>(RoleTypes.SchoolAdmin);

            /* Many to many relationships configuration */
            builder.Entity<ClassToSubject>()
                .HasKey(cts => cts.Id);
            builder.Entity<ClassToSubject>()
                .HasOne(cts => cts.Class)
                .WithMany(book => book.Subjects)
                .HasForeignKey(cts => cts.ClassId);
            builder.Entity<ClassToSubject>()
                .HasOne(cts => cts.Subject)
                .WithMany(subject => subject.Classes)
                .HasForeignKey(cts => cts.SubjectId);

            builder.Entity<StudentToGrade>()
                .HasKey(stg => stg.Id);
            builder.Entity<StudentToGrade>()
                .HasOne(stg => stg.Student)
                .WithMany(student => student.Grades)
                .HasForeignKey(sta => sta.StudentId);
            builder.Entity<StudentToGrade>()
                .HasOne(stg => stg.Grade)
                .WithMany(grade => grade.Students)
                .HasForeignKey(stg => stg.GradeId);

            builder.Entity<TeacherToSubject>()
                .HasKey(tts => new { tts.SubjectId, tts.TeacherId });
            builder.Entity<TeacherToSubject>()
                .HasOne(tts => tts.Teacher)
                .WithMany(teacher => teacher.Subjects)
                .HasForeignKey(tts => tts.TeacherId);
            builder.Entity<TeacherToSubject>()
                .HasOne(tts => tts.Subject)
                .WithMany(subject => subject.Teachers)
                .HasForeignKey(tts => tts.SubjectId);

            /* Many to one relationship configuration */

            builder.Entity<Class>()
                .HasOne(c => c.School)
                .WithMany(s => s.Classes)
                .IsRequired();

            builder.Entity<TeacherToSubject>()
                    .HasKey(tts => new
                    {
                        tts.SubjectId,
                        tts.TeacherId
                    });

            builder.Entity<SchoolUser>()
                .HasIndex(su => su.Pin)
                .IsUnique();


            // Additional Seeding - Comment after Update-Database
            builder.Entity<Subject>()
                .HasData(
                    new Subject { Id = Guid.NewGuid().ToString(), Name = "Български език и литература", GradeYear = 1 },
                    new Subject { Id = Guid.NewGuid().ToString(), Name = "Математика", GradeYear = 1 },
                    new Subject { Id = Guid.NewGuid().ToString(), Name = "Околен свят", GradeYear = 1 },
                    new Subject { Id = Guid.NewGuid().ToString(), Name = "Музика", GradeYear = 1 },
                    new Subject { Id = Guid.NewGuid().ToString(), Name = "Български език и литература", GradeYear = 2 },
                    new Subject { Id = Guid.NewGuid().ToString(), Name = "Математика", GradeYear = 2 },
                    new Subject { Id = Guid.NewGuid().ToString(), Name = "Английски език", GradeYear = 2 }
                );

            builder.Entity<Grade>()
                .HasData(
                    new Grade { Id = Guid.NewGuid().ToString(), ValueNum = 6, ValueWord = "Отличен" },
                    new Grade { Id = Guid.NewGuid().ToString(), ValueNum = 5, ValueWord = "Много добър" },
                    new Grade { Id = Guid.NewGuid().ToString(), ValueNum = 4, ValueWord = "Добър" },
                    new Grade { Id = Guid.NewGuid().ToString(), ValueNum = 3, ValueWord = "Среден" },
                    new Grade { Id = Guid.NewGuid().ToString(), ValueNum = 2, ValueWord = "Слаб" }
                );

            //builder.Entity<SchoolUser>()
            //    .HasData(
            //        new SchoolUser
            //        {
            //            FirstName = "Sladi",
            //            SecondName = "Sladkov",
            //            LastName = "Sladkov",
            //            Pin = "0510043827",
            //            Address = "Някъде от София",
            //            Town = "София",
            //            Role = RoleTypes.Student,
            //            School = Schools.FirstOrDefault(),
            //            //TODO : Add rest
            //        }

            //    );

            

        }
    }
}