using AutoMapper;
using AutoMapper.QueryableExtensions;
using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.ViewModels.Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            var dbClass = this.repo.Classes.GetById(classId);

            if (dbClass is null)
            {
                throw new TargetException("Class not found");
            }

            var teacher = repo.Teachers.GetById(teacherId);

            if (teacher is null)
            {
                throw new TargetException("Teacher not found");
            }

            dbClass.ClassTeacher = teacher;

            this.repo.Classes.Update(dbClass);
        }

        public void AddSubject(string classId, ClassToSubjectInputModel inputModel)
        {
            var dbClass = this.repo.Classes.Query()
                .Include(c => c.Subjects)
                .FirstOrDefault(c => c.Id == classId);

            if (dbClass is null)
            {
                throw new TargetException("Class not found");
            }

            var subject = repo.Subjects.GetById(inputModel.SubjectId);
            if (subject is null)
            {
                throw new TargetException("Subject not found");
            }

            var teacher = repo.TeacherToSubject.Query()
                .Include(tts => tts.Teacher)
                .Include(tts => tts.Subject)
                .Where(tts => tts.SubjectId == inputModel.SubjectId)
                .FirstOrDefault(tts => tts.TeacherId == inputModel.TeacherId);

            if (teacher is null)
            {
                throw new TargetException("Teacher not found");
            }

            dbClass.Subjects.Add(new ClassToSubject
            {
                Id = Guid.NewGuid().ToString(),
                Class = dbClass,
                ClassId = classId,
                Subject = subject,
                SubjectId = subject.Id,
                Teacher = teacher.Teacher,
                EndTime = inputModel.EndTime,
                StartTime = inputModel.StartTime,
                WeekDay = inputModel.WeekDay

            });

            repo.Classes.SaveChanges();
        }

        public ClassViewModel EditClass(string id, ClassInputModel inputModel)
        {
            var classFromDto = repo.Classes.GetWithoutTracking()
               .Find(c => c.Id == id);


            var editClass = new Class()
            {
                Id = classFromDto.Id,
                StartYear = inputModel.StartYear,
                Grade = inputModel.GradeYear,
                GradeLetter = inputModel.GradeLetter,
            };


            repo.Classes.Update(editClass);
            repo.SaveChanges();

            return new ClassViewModel()
            {
                StartYear = editClass.StartYear,
                Grade = editClass.Grade,
                GradeLetter = editClass.GradeLetter,
            };

        }

        public void EditSubject(string classId, ClassToSubjectInputModel inputModel)
        {
            var subject = repo.ClassToSubject.Query()
                .Include(cs => cs.Subject)
                .Include(cs => cs.Teacher)
                .AsNoTracking()
                .Where(cs => cs.ClassId == classId)
                .FirstOrDefault(cs => cs.SubjectId == inputModel.SubjectId);

            if (subject is null)
            {
                throw new TargetException("Data not found");
            }

            var teacher = repo.TeacherToSubject.Query()
                .Include(tts => tts.Teacher)
                .Include(tts => tts.Subject)
                .Where(tts => tts.SubjectId == inputModel.SubjectId)
                .FirstOrDefault(tts => tts.TeacherId == inputModel.TeacherId);

            if (teacher is null)
            {
                throw new TargetException("Teacher not found");
            }

            var newData = mapper.Map<ClassToSubjectInputModel, ClassToSubject>(inputModel);
            newData.Id = subject.Id;
            newData.ClassId = classId;
            newData.SubjectId = subject.SubjectId;
            newData.Teacher = teacher.Teacher;

            repo.ClassToSubject.Update(newData);
        }

        public List<ClassViewModel> GetAll()
        {
            List<ClassViewModel> classViewList = new List<ClassViewModel>();


            var classes = repo.Classes.Query()
                .Include(c => c.ClassTeacher)
                .Include(c => c.Subjects)
                .ToList();

            //if (!classes.Any())
            //{
            //    throw new TargetException("No classes found");
            //}

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
