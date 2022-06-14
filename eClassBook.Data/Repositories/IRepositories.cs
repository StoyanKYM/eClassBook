using eClassBook.Models;
using eClassBook.Models.SchoolUserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Data.Repositories
{
    public interface IRepositories
    {
        IGeneralRepository<Absence> Absences { get; }

        IGeneralRepository<Class> Classes { get; }

        IGeneralRepository<Grade> Grades { get; }

        IGeneralRepository<Parent> Parents { get; }

        IGeneralRepository<Principal> Principals { get; }

        IGeneralRepository<School> Schools { get; }

        IGeneralRepository<Student> Students { get; }

        IGeneralRepository<Subject> Subjects { get; }

        IGeneralRepository<Teacher> Teachers { get; }

        IGeneralRepository<SchoolAdmin> SchoolAdmins { get; }

        IGeneralRepository<ApplicationUser> Users { get; }

        IGeneralRepository<SchoolUser> SchoolUsers { get; }

        IGeneralRepository<ClassToSubject> ClassToSubject { get; }

        IGeneralRepository<StudentToGrade> StudentsToGrades { get; }

        IGeneralRepository<TeacherToSubject> TeacherToSubject { get; }

        int SaveChanges();
    }
}
