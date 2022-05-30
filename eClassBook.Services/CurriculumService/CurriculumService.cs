using AutoMapper;
using AutoMapper.QueryableExtensions;
using eClassBook.Data.Repositories;
using eClassBook.ViewModels.Class;
using eClassBook.ViewModels.Student;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eClassBook.Services.CurriculumService
{
    public class CurriculumService : ICurriculumService
    {
        private readonly IRepositories repo;
        private readonly IMapper mapper;

        public CurriculumService(
            IRepositories repo,
            IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public List<StudentViewModel> GetStudentsInClassAttendingSubject(string classCurriculumId)
        {
            var classToSubject = this.repo.ClassToSubject.Query()
                .FirstOrDefault(cts => cts.Id == classCurriculumId);

            var students = this.repo.Students.Query()
                .Include(s => s.Class)
                .Where(s => s.Class.Id == classToSubject.ClassId)
                .ProjectTo<StudentViewModel>(mapper.ConfigurationProvider)
                .ToList();

            if (students is null || !students.Any())
            {
                throw new TargetException("Couldn't find any data for students in this class.");
            }

            return students;
        }

        public List<StudentViewModel> GetStudentWeeklyCurriculum(string classCurriculumId)
        {
            var classToSubject = this.repo.ClassToSubject.Query()
                .FirstOrDefault(cts => cts.Id == classCurriculumId);

            var students = this.repo.Students.Query()
                .Include(s => s.Class)
                .Where(s => s.Class.Id == classToSubject.ClassId)
                .ProjectTo<StudentViewModel>(mapper.ConfigurationProvider)
                .ToList();

            //DbContextOptionsBuilder.EnableSensitiveDataLogging'.'DbContextOptionsBuilder.EnableSensitiveDataLogging'.'

            if (students is null || !students.Any())
            {
                throw new TargetException("Couldn't find any data for students in this class.");
            }

            return students;
        }

        // is this correct?
        public List<ClassToSubjectViewModel> GetTeacherActiveSubjects(string teacherId)
        {
            var classToSubjects = this.repo.ClassToSubject.Query()
                .Include(cts => cts.Class)
                .Include(cts => cts.Subject)
                .Include(cts => cts.Teacher)
                .Where(cts => cts.Teacher.Id == teacherId)
                .ProjectTo<ClassToSubjectViewModel>(mapper.ConfigurationProvider)
                .ToList();

            if (classToSubjects is null)
            {
                throw new TargetException("Couldn't find any data for subjects by this teacher.");
            }

            return classToSubjects;
        }
    }
}
