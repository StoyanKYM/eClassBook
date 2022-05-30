using AutoMapper;
using AutoMapper.QueryableExtensions;
using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.StatisticsService
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepositories repo;
        private readonly IMapper mapper;

        public StatisticsService(
            IRepositories repo,
            IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }
        public ICollection<StatisticInfoDTO> AverageSubjectScores(string schoolId)
        {
            var subjectScores = new List<StatisticInfoDTO>();

            var subjects = repo.TeacherToSubject.Query()
                .Include(cts => cts.Subject)
                .Include(cts => cts.Teacher)
                .ThenInclude(t => t.School)
                .Where(cts => cts.Teacher.School.Id == schoolId)
                .ToList();

            if (!subjects.Any())
            {
                throw new TargetException("No data found for subject scores");
            }

            foreach (var s in subjects)
            {
                var grades = repo.StudentsToGrades.Query()
                    .Include(stg => stg.Grade)
                    .Where(stg => stg.SubjectId == s.SubjectId)
                    .ProjectTo<double>(mapper.ConfigurationProvider)
                    .ToList();

                var avg = grades.Sum() / grades.Count();

                subjectScores.Add(new StatisticInfoDTO
                {
                    Name = s.Subject.Name + " ("
                                          + s.Teacher.FirstName + " " +
                                          s.Teacher.SecondName.Substring(0, 1)
                                          + ". " + s.Teacher.LastName + ")",
                    Value = avg,
                });
            }

            return subjectScores;
        }

        public IDictionary<string, double> AverageSubjectScores()
        {
            var subjectScores = new Dictionary<string, double>();

            var subjects = repo.StudentsToGrades.Query()
                .AsNoTracking()
                .Include(stg => stg.Subject)
                .ProjectTo<Subject>(mapper.ConfigurationProvider)
                .Distinct()
                .ToList();

            foreach (var s in subjects)
            {
                var grades = repo.StudentsToGrades.Query()
                    .AsNoTracking()
                    .Include(stg => stg.Grade)
                    .Where(stg => stg.SubjectId == s.Id)
                    .ProjectTo<double>(mapper.ConfigurationProvider)
                    .ToList();

                subjectScores.Add(s.Name, grades.Sum() / grades.Count);
            }

            return subjectScores;
        }

        public ICollection<StatisticInfoDTO> AverageTeacherScores(string schoolId)
        {
            var teacherScores = new List<StatisticInfoDTO>();

            var teachers = repo.StudentsToGrades.Query()
                .Include(stg => stg.Teacher)
                .ThenInclude(t => t.School)
                .Where(stg => stg.Teacher.School.Id == schoolId)
                .ProjectTo<Teacher>(mapper.ConfigurationProvider)
                .Distinct()
                .ToList();

            if (!teachers.Any())
            {
                throw new TargetException("No data found for teacher scores");
            }

            foreach (var t in teachers)
            {
                var grades = repo.StudentsToGrades.Query()
                    .Include(stg => stg.Grade)
                    .Include(stg => stg.Teacher)
                    .Where(stg => stg.Teacher.Id == t.Id)
                    .ProjectTo<double>(mapper.ConfigurationProvider)
                    .ToList();

                var avg = grades.Sum() / grades.Count();
                var tName = t.FirstName.ToString() + " " +
                            t.SecondName.Substring(0, 1) + ". " +
                            t.LastName;

                teacherScores.Add(new StatisticInfoDTO { Name = tName, Value = avg });
            }

            return teacherScores;
        }

        public IDictionary<string, double> AverageTeacherScores()
        {
            var teacherScores = new Dictionary<string, double>();

            var teachers = repo.StudentsToGrades.Query()
                .AsNoTracking()
                .Include(stg => stg.Teacher)
                .OrderBy(stg => stg.Teacher.School.Id)
                .ProjectTo<Teacher>(mapper.ConfigurationProvider)
                .Distinct()
                .ToList();

            foreach (var t in teachers)
            {
                var grades = repo.StudentsToGrades.Query()
                    .AsNoTracking()
                    .Include(stg => stg.Grade)
                    .Where(stg => stg.Teacher.Id == t.Id)
                    .ProjectTo<double>(mapper.ConfigurationProvider)
                    .ToList();

                var tName = t.FirstName.ToString() + " " +
                            t.SecondName.Substring(0, 1) + ". " +
                            t.LastName;

                teacherScores.Add(tName, grades.Sum() / grades.Count);
            }

            return teacherScores;
        }

        public ICollection<StatisticInfoDTO> SchoolAbsences(string schoolId)
        {
            var absencesDictionary = new List<StatisticInfoDTO>(3);

            var absences = repo.Absences.Query()
                .Include(a => a.Student)
                .ThenInclude(s => s.School)
                .Where(a => a.Student.School.Id == schoolId)
                .ToList();

            if (!absences.Any())
            {
                throw new TargetException("No data found for school's absences");
            }

            absencesDictionary.Add(new StatisticInfoDTO
            {
                Name = "Excused Full Absences",
                Value = absences.Count(a => a.IsExcused && a.IsFullAbsence)
            });
            absencesDictionary.Add(new StatisticInfoDTO
            {
                Name = "Unexcused Full Absences",
                Value = absences.Count(a => !a.IsExcused && a.IsFullAbsence)
            });
            absencesDictionary.Add(new StatisticInfoDTO
            {
                Name = "Unexcused Half Absences",
                Value = absences.Count(a => !a.IsExcused && !a.IsFullAbsence)
            });

            return absencesDictionary;
        }

        public IDictionary<string, ICollection<StatisticInfoDTO>> SchoolAbsences()
        {
            throw new NotImplementedException();
        }

        public double SchoolAverageScore(string schoolId)
        {
            var grades = repo.StudentsToGrades.Query()
                .AsNoTracking()
                .Include(stg => stg.Grade)
                .ProjectTo<int>(mapper.ConfigurationProvider)
                .ToList();

            return grades.Sum() / grades.Count;
        }

        

        public double SchoolAverageScore()
        {
            var grades = repo.StudentsToGrades.Query()
                .AsNoTracking()
                .Include(stg => stg.Grade)
                .ProjectTo<int>(mapper.ConfigurationProvider)
                .ToList();

            return grades.Sum() / grades.Count;
        }

        public IDictionary<string, int> StudentAbsences(string studentId)
        {
            var absencesDictionary = new Dictionary<string, int>(3);

            var absences = repo.Absences.Query()
                .Include(a => a.Student)
                .Where(a => a.Student.Id == studentId)
                .ToList();

            if (!absences.Any())
            {
                throw new TargetException("No data found for school's absences");
            }

            absencesDictionary.Add("Excused Full Absences", absences.Count(a => a.IsExcused && a.IsFullAbsence));
            absencesDictionary.Add("Unexcused Full Absences", absences.Count(a => !a.IsExcused && a.IsFullAbsence));
            absencesDictionary.Add("Unexcused Half Absences", absences.Count(a => !a.IsExcused && !a.IsFullAbsence));

            return absencesDictionary;
        }

        public double StudentAverageScore(string studentId)
        {
            var grades = repo.StudentsToGrades.Query()
                .Include(stg => stg.Grade)
                .Include(stg => stg.Subject)
                .Where(stg => stg.StudentId == studentId)
                .ProjectTo<double>(mapper.ConfigurationProvider)
                .ToList();

            if (grades.Count <= 0)
            {
                throw new ArithmeticException("No grades registered for this student");
            }

            return grades.Sum() / grades.Count();
        }

        public double TeacherAverageScore(string teacherId)
        {
            var grades = repo.StudentsToGrades.Query()
                .Include(stg => stg.Grade)
                .Include(stg => stg.Subject)
                .Include(stg => stg.Teacher)
                .Where(stg => stg.Teacher.Id == teacherId)
                .ProjectTo<double>(mapper.ConfigurationProvider)
                .ToList();

            if (grades.Count <= 0)
            {
                throw new ArithmeticException("No grades registered for this teacher");
            }

            return grades.Sum() / grades.Count();
        }
    }
}
