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
        public List<ClassToSubjectViewModel> TeacherActiveSubjects([FromRoute] string teacherId)
        {
            return this.curriculumService.GetTeacherActiveSubjects(teacherId);
        }

        // is this correct?
        [HttpGet("StudentWeeklyCurriculum/{studentId}")]
        public List<StudentViewModel> StudentWeeklyCurriculum([FromRoute] string studentId)
        {
            return this.curriculumService.GetStudentWeeklyCurriculum(studentId);
        }
    }
}
