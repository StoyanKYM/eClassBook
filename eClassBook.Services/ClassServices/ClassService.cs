using AutoMapper;
using AutoMapper.QueryableExtensions;
using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.ViewModels.Class;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eClassBook.Services.ClassServices
{
    public class ClassService : IClassService
    {
        private readonly IRepositories repo;
        private readonly IMapper mapper;

        public ClassService(IRepositories repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public void CreateClass(ClassInputModel inputModel)
        {
            var entityClass = mapper.Map<ClassInputModel, Class>(inputModel);


            entityClass.SchoolId = repo.Schools.Query()
                .AsNoTracking()
                .FirstOrDefault(s => s.Id == inputModel.SchoolId)?.Id;


            this.repo.Classes.Create(entityClass);
        }

        public void AddClassTeacher(string classId, string teacherId)
        {
            throw new NotImplementedException("Implement later");
        }

        public void AddSubject(string classId, ClassToSubjectInputModel inputModel)
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public ClassViewModel EditClass(string id, ClassInputModel inputModel)
        {
            throw new NotImplementedException("Not Implemented.");

        }

        public void EditSubject(string classId, ClassToSubjectInputModel inputModel)
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public List<ClassViewModel> GetAll()
        {
            List<ClassViewModel> classViewList = new List<ClassViewModel>();


            var classes = repo.Classes.Query()
                .Include(c => c.ClassTeacher)
                .Include(c => c.Subjects)
                .ToList();


            foreach (var item in classes)
            {
                var current = new ClassViewModel()
                {
                    StartYear = item.StartYear,
                    ClassTeacher = item.ClassTeacher,
                    // is this correct?
                    Grade = item.Grade,
                    GradeLetter = item.GradeLetter,
                };

                classViewList.Add(current);
            }

            
            return classViewList;
        }

        public List<ClassViewModel> GetAllByGrade(int grade)
        {
            var classes = this.repo.Classes.Query()
                .Include(c => c.ClassTeacher)
                .Include(c => c.Subjects)
                .Where(c => c.GradeLetter == grade)
                .OrderBy(c => c.GradeLetter)
                .ProjectTo<ClassViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            if (!classes.Any())
            {
                throw new TargetException("No classes found");
            }

            return classes;
        }

        public List<ClassViewModel> GetAllBySchool(string schoolId)
        {
            return repo.Classes.Query()
                .Include(c => c.School)
                .Include(c => c.ClassTeacher)
                .Where(c => c.School.Id == schoolId)
                .ProjectTo<ClassViewModel>(mapper.ConfigurationProvider)
                .ToList();
        }

        public List<ClassViewModel> GetClassesWithoutClassTeacher(string schoolId)
        {
            throw new NotImplementedException();
        }

        public ClassViewModel GetClassById(string id)
        {
            var dbClass = this.repo.Classes.GetById(id);

            if (dbClass is null)
            {
                throw new TargetException("Class not found");
            }

            var searchedClass = mapper.Map<Class, ClassViewModel>(dbClass);

            return searchedClass;
        }

        public void RemoveSubject(string classId, string subjectId)
        {
            var dbClass = this.repo.Classes.Query()
               .Include(c => c.Subjects)
               .FirstOrDefault(c => c.Id == classId);

            if (dbClass is null)
            {
                throw new TargetException("Class not found");
            }

            var subject = dbClass.Subjects.FirstOrDefault(s => s.SubjectId == subjectId);

            if (subject is null)
            {
                throw new TargetException("Subject not found");
            }

            dbClass.Subjects.Remove(subject);
            repo.Classes.SaveChanges();
        }
    }
}
