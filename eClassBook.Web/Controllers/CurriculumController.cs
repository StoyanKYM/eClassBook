using eClassBook.Services.CurriculumService;
using eClassBook.ViewModels.Class;
using eClassBook.ViewModels.Student;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Curriculum/")]
    public class CurriculumController : Controller
    {
        private readonly ICurriculumService curriculumService;

        public CurriculumController(ICurriculumService curriculumService)
        {
            this.curriculumService = curriculumService;
        }

        [HttpGet("TeacherActiveSubjects/{teacherId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal, Teacher")]
        public List<ClassToSubjectViewModel> TeacherActiveSubjects([FromRoute] string teacherId)
        {
            return this.curriculumService.GetTeacherActiveSubjects(teacherId);
        }


        [HttpGet("StudentsAttendingSubject/{classCurriculumId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal, Teacher")]
        public List<StudentViewModel> StudentsAttendingSubject([FromRoute] string classCurriculumId)
        {
            return this.curriculumService.GetStudentsInClassAttendingSubject(classCurriculumId);
        }

        // is this correct?
        [HttpGet("StudentWeeklyCurriculum/{studentId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal, Teacher, Student, Parent")]
        public List<StudentViewModel> StudentWeeklyCurriculum([FromRoute] string studentId)
        {
            return this.curriculumService.GetStudentWeeklyCurriculum(studentId);
        }
    }
}
