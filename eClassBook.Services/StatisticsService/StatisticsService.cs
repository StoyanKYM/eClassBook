using AutoMapper;
using AutoMapper.QueryableExtensions;
using eClassBook.Data.Repositories;
using eClassBook.Services.DTOs;
using Microsoft.EntityFrameworkCore;

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
            throw new NotImplementedException("Not Implemented.");
        }

        public IDictionary<string, double> AverageSubjectScores()
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public ICollection<StatisticInfoDTO> AverageTeacherScores(string schoolId)
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public IDictionary<string, double> AverageTeacherScores()
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public ICollection<StatisticInfoDTO> SchoolAbsences(string schoolId)
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public IDictionary<string, ICollection<StatisticInfoDTO>> SchoolAbsences()
        {
            throw new NotImplementedException("Not Implemented.");
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
            throw new NotImplementedException("Not Implemented.");
        }

        public double StudentAverageScore(string studentId)
        {
            throw new NotImplementedException("Not Implemented.");
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
