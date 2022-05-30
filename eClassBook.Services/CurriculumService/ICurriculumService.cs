using eClassBook.ViewModels.Class;
using eClassBook.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.CurriculumService
{
    public interface ICurriculumService
    {
        List<ClassToSubjectViewModel> GetTeacherActiveSubjects(string teacherId);

        List<StudentViewModel> GetStudentsInClassAttendingSubject(string classCurriculumId);

        List<StudentViewModel> GetStudentWeeklyCurriculum(string studentId);
    }
}
