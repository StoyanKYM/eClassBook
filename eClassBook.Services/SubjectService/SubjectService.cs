using AutoMapper;
using AutoMapper.QueryableExtensions;
using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.ViewModels.Subject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.SubjectService
{
    public  class SubjectService : ISubjectService
    {
        private readonly IRepositories repo;
        private readonly IMapper mapper;

        public SubjectService(
            IRepositories repo,
            IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        public void CreateSubject(SubjectInputModel inputModel)
        {
            var subject = mapper.Map<SubjectInputModel, Subject>(inputModel);
            repo.Subjects.Create(subject);
        }

        // rework later
        public void AddTeacherToSubject(string subjectId, string teacherId)
        {
            var subject = repo.Subjects.GetById(subjectId);

            if (subject is null)
            {
                throw new TargetException("Subject isn't in our system.");
            }

            var teacher = repo.Teachers.GetById(teacherId);
            teacher.Subjects.Add(new TeacherToSubject
            {
                Teacher = teacher,
                TeacherId = teacherId,
                Subject = subject,
                SubjectId = subjectId
            });

            repo.Teachers.SaveChanges();
        }

        public void DeleteSubject(string id)
        {
            var subject = repo.Subjects.GetById(id);

            if (subject is null)
            {
                throw new TargetException("Subject isn't in our system.");
            }

            repo.Subjects.Delete(subject);
            repo.Subjects.SaveChanges();
        }

        public SubjectViewModel EditSubject(string id, SubjectInputModel inputModel)
        {
            var subjectFromDb = repo.Subjects.GetWithoutTracking()
                .Find(s => s.Id == id);

            if (subjectFromDb is null)
            {
                throw new TargetException("Subject not found");
            }

            var subject = mapper.Map<SubjectInputModel, Subject>(inputModel);
            subject.Id = subjectFromDb.Id;

            repo.Subjects.Update(subject);
            repo.SaveChanges();

            return mapper.Map<Subject, SubjectViewModel>(subject);
        }

        // Fix later
        public List<SubjectViewModel> GetAll()
        {
            var subjects = this.repo.Subjects.Query()
               .Include(s => s.Classes)
               .Include(s => s.Teachers)
               .OrderBy(s => s.Name)
               .ProjectTo<SubjectViewModel>(this.mapper.ConfigurationProvider)
               .ToList();

            if (!subjects.Any())
            {
                throw new TargetException("No subjects found");
            }

            return subjects;
        }

        public List<SubjectViewModel> GetAllByGradeYear(int grade)
        {
            var subjects = this.repo.Subjects.Query()
                .Include(s => s.Classes)
                .Include(s => s.Teachers)
                .Where(s => s.GradeYear == grade)
                .OrderBy(s => s.Name)
                .ProjectTo<SubjectViewModel>(this.mapper.ConfigurationProvider)
                .ToList();

            if (!subjects.Any())
            {
                throw new TargetException("No subjects found");
            }

            return subjects;
        }

        public List<SubjectViewModel> GetAllByTeacherId(string teacherId)
        {
            var classToSubjects = this.repo.ClassToSubject.Query()
                .Include(cts => cts.Class)
                .Include(cts => cts.Subject)
                .Include(cts => cts.Teacher)
                .Where(cts => cts.Teacher.Id == teacherId)
                .ProjectTo<SubjectViewModel>(mapper.ConfigurationProvider)
                .ToList();

            //            if (classToSubjects is null)
            //            {
            //                throw new TargetException("Couldn't find any data for subjects by this teacher.");
            //            }    

            return classToSubjects;
        }

        public SubjectViewModel GetSubjectById(string id)
        {
            var subject = this.repo.Subjects.Query()
                .Include(s => s.Classes)
                .Include(s => s.Teachers)
                .ProjectTo<SubjectViewModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefault(s => s.Id == id);

            if (subject is null)
            {
                throw new TargetException("Subject not found");
            }

            return subject;
        }

        public List<SubjectViewModel> GetStudentsAttending(string subjectId)
        {
            throw new NotImplementedException();
        }

        public void RemoveTeacherFromSubject(string subjectId, string teacherId)
        {
            var subject = this.repo.Subjects.Query()
                .Include(s => s.Classes)
                .Include(s => s.Teachers)
                .FirstOrDefault(s => s.Id == subjectId);

            if (subject is null)
            {
                throw new TargetException("Subject isn't in our system.");
            }

            var teacher = subject.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
            //            teacher.Subjects.Remove(toBeDeleted);
            subject.Teachers.Remove(teacher);

            repo.Subjects.SaveChanges();
        }
    }
}
