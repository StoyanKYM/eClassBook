using AutoMapper;
using AutoMapper.QueryableExtensions;
using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.Services.AccountServices;
using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.StatisticsService;
using eClassBook.ViewModels.Absence;
using eClassBook.ViewModels.Grade;
using eClassBook.ViewModels.Student;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace eClassBook.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly IRepositories repo;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IStatisticsService statistics;

        public StudentService(
            IRepositories repositories, IAccountService accountService, IMapper mapper, IStatisticsService statistics)
        {
            this.repo = repositories;
            this._accountService = accountService;
            this._mapper = mapper;
            this.statistics = statistics;
        }
        public async Task AddStudent(StudentDTO studentModel)
        {
            if (repo.SchoolUsers.Query().Any(su => su.Pin == studentModel.Pin))
            {
                throw new DuplicateNameException("User already exists");
            }

            var addStudent = new Student()
            {
                FirstName = studentModel.FirstName,
                LastName = studentModel.LastName,
                SecondName = studentModel.SecondName,
                Pin = studentModel.Pin,
                Address = studentModel.Address,
                Town = studentModel.Town,
                StartYear = studentModel.StartYear,
                Role = Models.Enumerations.RoleTypes.Student,
            };


            var studentSchool = repo.Schools.GetById(studentModel.SchoolId);

            if (studentSchool == null)
            {
                throw new TargetException("School does not exist");
            }

            var studentClass = repo.Classes.GetById(studentModel.ClassId);

            if (studentClass == null)
            {
                throw new TargetException("Class does not exist");
            }

            addStudent.Class = studentClass;
            addStudent.School = studentSchool;

            var account = await _accountService.RegisterSchoolUser(addStudent);
            addStudent.User = account;
            addStudent.Id = addStudent.User.Id;
            repo.Students.Create(addStudent);
            repo.SaveChanges();
        }

        public IEnumerable<StudentViewModel> GetAllStudents()
        {
            var listOfStudents = new List<StudentViewModel>();
            var students = repo.Students.Query().ToList();

            foreach (var student in students)
            {
                var currentStudent = new StudentViewModel()
                {
                    FirstName = student.FirstName,
                    SecondName = student.SecondName,
                    LastName = student.LastName,
                    SchoolId = student.SchoolId.ToString(),
                    ClassId = student.ClassId,
                    StartYear = student.StartYear,
                    Town = student.Town,
                };

                listOfStudents.Add(currentStudent);
            }

            return listOfStudents;
        }

        public IEnumerable<StudentDTO> GetAllStudentsFromClass(string classId)
        {
            var students = repo.Students.Query()
                .Include(o => o.School)
                .Include(o => o.Class)
                .Include(o => o.User)
                .Where(s => s.Class.Id == classId)
                .ProjectTo<StudentDTO>(_mapper.ConfigurationProvider);

            return students;
        }

        public StudentDTO GetStudent(string id)
        {
            var student = repo.Students.Query()
                .Include(o => o.School)
                .Include(o => o.Class)
                .Include(o => o.User)
                .SingleOrDefault(o => o.Id == id);

            return _mapper.Map<Student, StudentDTO>(student);
        }

        public void EditGrade(string gradeId, string newGradeId)
        {
           throw new NotImplementedException("Not Implemented");
        }

        public void RemoveGrade(string gradeId)
        {
            var grade = repo.StudentsToGrades.GetById(gradeId);

            repo.StudentsToGrades.Delete(grade);
            repo.StudentsToGrades.SaveChanges();
        }

        public void ExcuseStudentAbsence(string studentId, string absenceId)
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public void RemoveStudent(string studentId)
        {
            var student = repo.Students.GetById(studentId);

            if (student is null)
            {
                throw new TargetException("Student not found.");
            }

            repo.Students.Delete(student);
            repo.Students.SaveChanges();
        }

        public IEnumerable<StudentTableViewModel> GetAllStudentsFromSchool(string schoolId)
        {
            var students = repo.Students.Query()
                .Include(o => o.School)
                .Include(o => o.Class)
                .Include(o => o.User)
                .Where(s => s.School.Id == schoolId)
                .ProjectTo<StudentTableViewModel>(_mapper.ConfigurationProvider)
                .ToList();

            return students;
        }

        public StudentDialogViewModel GetStudentDialogData(string id)
        {
            var st = repo.Students.Query()
                .AsNoTracking()
                .Include(s => s.Class)
                .Include(s => s.Parent)
                .Include(s => s.User)
                .FirstOrDefault(s => s.Id == id);

            var student = _mapper.Map<Student, StudentDialogViewModel>(st);

            //student.AvgScore = statistics.StudentAverageScore(id);
            //student.Absences = statistics.StudentAbsences(id);

            return student;
        }

        public void UpdateStudent(string studentId, StudentEditInputModel studentModel)
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public void GradeStudent(string studentId, GradeInputModel gradeModel)
        {
            throw new NotImplementedException("Not Implemented.");
        }

        public void AddAbsenceToStudent(string studentId, AbsenceInputModel absenceModel)
        {
            throw new NotImplementedException("Not Implemented.");
        }
    }
}
