using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.ViewModels.Absence;
using eClassBook.ViewModels.Grade;
using eClassBook.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.StudentService
{
    public interface IStudentService
    {
        IEnumerable<StudentViewModel> GetAllStudents();

        IEnumerable<StudentTableViewModel> GetAllStudentsFromSchool(string schoolId);

        IEnumerable<StudentDTO> GetAllStudentsFromClass(string classId);

        StudentDTO GetStudent(string id);

        StudentDialogViewModel GetStudentDialogData(string id);

        Task AddStudent(StudentDTO studentModel);

        void UpdateStudent(string studentId, StudentEditInputModel studentModel);

        void GradeStudent(string studentId, GradeInputModel gradeModel);

        void EditGrade(string gradeId, string newGradeId);

        void RemoveGrade(string gradeId);

        void AddAbsenceToStudent(string studentId, AbsenceInputModel absenceModel);

        void ExcuseStudentAbsence(string studentId, string absenceId);

        void RemoveStudent(string studentId);
    }
}
