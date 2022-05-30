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

        //Add mapper
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
            var grade = repo.StudentsToGrades.Query()
                .Include(stg => stg.Grade)
                .SingleOrDefault(stg => stg.Id == gradeId);

            if (grade is null)
            {
                throw new TargetException("Grade not found");
            }

            grade.GradeId = newGradeId;
            grade.DateModified = DateTime.Now;

            repo.StudentsToGrades.SaveChanges();
        }

        public void RemoveGrade(string gradeId)
        {
            var grade = repo.StudentsToGrades.GetById(gradeId);

            repo.StudentsToGrades.Delete(grade);
            repo.StudentsToGrades.SaveChanges();
        }

        public void ExcuseStudentAbsence(string studentId, string absenceId)
        {
            var student = repo.Students.Query()
                .Include(s => s.Absences)
                .SingleOrDefault(s => s.Id == studentId);

            if (student is null)
            {
                throw new TargetException("Student not found");
            }

            var absence = student.Absences.SingleOrDefault(a => a.Id == absenceId);

            if (absence is null)
            {
                throw new TargetException("Student absence not found");
            }

            if (!absence.IsFullAbsence)
            {
                throw new ArgumentException("Cannot excuse absences that aren't full.");
            }

            absence.IsExcused = true;
            absence.DateModified = DateTime.Now;

            repo.Students.SaveChanges();
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

            student.AvgScore = statistics.StudentAverageScore(id);
            student.Absences = statistics.StudentAbsences(id);

            return student;
        }

        public void UpdateStudent(string studentId, StudentEditInputModel studentModel)
        {
            var student = repo.Students.Query()
                .AsNoTracking()
                .SingleOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                throw new TargetException("Student does not exist");
            }

            // is this correct?
            var newData = _mapper.Map<StudentEditInputModel, Student>(studentModel);
            newData.Id = studentId;
            newData.StartYear = student.StartYear;

            repo.Students.Update(newData);
        }

        public void GradeStudent(string studentId, GradeInputModel gradeModel)
        {
            var student = repo.Students.GetById(studentId);

            var grade = repo.Grades.GetWithoutTracking()
                .SingleOrDefault(g => g.Id == gradeModel.GradeId);

            var ts = repo.TeacherToSubject.Query()
                .AsNoTracking()
                .Include(tts => tts.Teacher)
                .Include(tts => tts.Subject)
                .SingleOrDefault(tts => tts.TeacherId == gradeModel.TeacherId
                                        && tts.SubjectId == gradeModel.SubjectId);

            if (ts is null) throw new TargetException("Invalid input data");

            var subject = repo.Subjects.GetWithoutTracking()
                .SingleOrDefault(s => s.Id == gradeModel.SubjectId);

            var teacher = repo.Teachers.GetWithoutTracking()
                .SingleOrDefault(t => t.Id == gradeModel.TeacherId);

            var newGrade = new StudentToGrade
            {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                GradeId = gradeModel.GradeId,
                Grade = grade,
                SubjectId = gradeModel.SubjectId,
                Subject = subject,
                StudentId = studentId,
                Student = student,
                Teacher = teacher
            };
            student.Grades.Add(newGrade);

            repo.Students.SaveChanges();
        }

        public void AddAbsenceToStudent(string studentId, AbsenceInputModel absenceModel)
        {
            var student = repo.Students.Query()
               .SingleOrDefault(s => s.Id == studentId);

            if (student is null)
            {
                throw new TargetException("Student not found");
            }

            var subject = repo.Subjects.GetWithoutTracking()
                .SingleOrDefault(s => s.Id == absenceModel.SubjectId);
            var teacher = repo.Teachers.GetWithoutTracking()
                .SingleOrDefault(t => t.Id == absenceModel.TeacherId);

            var absence = new Absence
            {
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                Student = student,
                Subject = subject,
                IsFullAbsence = absenceModel.IsFullAbsence,
                IsExcused = false,
                Teacher = teacher
            };

            student.Absences.Add(absence);

            repo.Students.SaveChanges();
        }
    }
}
