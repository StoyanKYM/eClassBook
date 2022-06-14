using AutoMapper;
using AutoMapper.QueryableExtensions;
using eClassBook.Data.Repositories;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.Services.AccountServices;
using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.StatisticsService;
using eClassBook.ViewModels.Class;
using eClassBook.ViewModels.Teacher;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace eClassBook.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly IRepositories repo;
        private readonly IMapper mapper;
        private readonly IAccountService accountService;
        private readonly IStatisticsService statistics;

        public TeacherService(IRepositories repo, IMapper mapper, IAccountService accountService, IStatisticsService statistics)
        {
            this.repo = repo;
            this.mapper = mapper;
            this.accountService = accountService;
            this.statistics = statistics;
        }
        
        public async Task AddTeacher(TeacherDTO teacher)
        {
            var teacherMap = mapper.Map<TeacherDTO, Teacher>(teacher);
            var school = repo.Schools.GetById(teacherMap.SchoolId);

            if (school == null)
            {
                throw new TargetException("School not found");
            }
            teacherMap.School = school;
            teacherMap.Role = Models.Enumerations.RoleTypes.Teacher;

            var account = await accountService.RegisterSchoolUser(teacherMap);
            if (account == null)
            {
                return;
            }
            teacherMap.User = account;
            teacherMap.Id = account.Id;
            repo.Teachers.Create(teacherMap);
        }
        // is this correct?
        public IEnumerable<TeacherTableViewModel> GetAllTeachersFromSchool(string schoolId)
        {
            var teachers = repo.Teachers.Query()
                .AsNoTracking()
                .Include(t => t.School)
                .Where(t => t.School.Id == schoolId)
                .ProjectTo<TeacherTableViewModel>(mapper.ConfigurationProvider)
                .ToList();

            var classes = repo.Classes.Query()
                .AsNoTracking()
                .Include(c => c.ClassTeacher)
                .ThenInclude(ct => ct.School)
                .Where(c => c.ClassTeacher.School.Id == schoolId)
                .ProjectTo<ClassTeacherDTO>(mapper.ConfigurationProvider)
                .ToList();

            foreach (var t in teachers)
            {
                if (classes.Any(c => c.TeacherId == t.SchoolUserId))
                {
                    t.Grade = classes.FirstOrDefault(c => c.TeacherId == t.SchoolUserId)?.Class;
                }
                else
                {
                    t.Grade = "-";
                }
            }

            return teachers;
        }

        public IEnumerable<MinimalSchoolUserDTO> GetAllTeachersFromSchoolDropdown(string schoolId)
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public IEnumerable<MinimalSchoolUserDTO> GetAllUnassignedToClass(string schoolId)
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public TeacherDialogViewModel GetTeacherDialogData(string teacherId)
        {
            var teacher = repo.Teachers.Query()
                .AsNoTracking()
                .Include(t => t.User)
                .Include(t => t.Subjects)
                .ThenInclude(s => s.Subject)
                .ProjectTo<TeacherDialogViewModel>(mapper.ConfigurationProvider)
                .FirstOrDefault(t => t.SchoolUserId == teacherId);

            var grade = repo.Classes.Query()
                .AsNoTracking()
                .Include(c => c.ClassTeacher)
                .ThenInclude(ct => ct.School)
                .Where(c => c.ClassTeacher.Id == teacherId)
                .ProjectTo<string>(mapper.ConfigurationProvider)
                .FirstOrDefault();

            if (grade is null)
            {
                teacher.Grade = "-";
            }

            teacher.Grade = grade;
            teacher.AvgScore = statistics.TeacherAverageScore(teacherId);

            return teacher;
        }

        public IEnumerable<MinimalSchoolUserDTO> GetTeachersListFromSubject(string subjectId)
        {
            var teachers = repo.TeacherToSubject.Query()
                .AsNoTracking()
                .Include(t => t.Teacher)
                .Where(t => t.SubjectId == subjectId)
                .ProjectTo<MinimalSchoolUserDTO>(mapper.ConfigurationProvider)
                .ToList();

            if (!teachers.Any())
            {
                throw new TargetException("No teachers found within this school");
            }

            return teachers;
        }
    }
}
