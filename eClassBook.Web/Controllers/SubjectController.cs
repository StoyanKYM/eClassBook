using eClassBook.Services.SubjectService;
using eClassBook.ViewModels.Subject;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Subject/")]
    public class SubjectController : Controller
    {
        private readonly ISubjectService subjectService;

        public SubjectController(
            ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        [HttpGet("GetAll")]
        public List<SubjectViewModel> GetAll()
        {
            return subjectService.GetAll();
        }

        [HttpGet("GetAllByGradeYear/{year}")]
        public List<SubjectViewModel> GetAllByGradeYear([FromRoute] int year)
        {
            return subjectService.GetAllByGradeYear(year);
        }

        [HttpGet("GetAllByTeacher/{teacherId}")]
        public List<SubjectViewModel> GetAllByTeacherId([FromRoute] string teacherId)
        {
            return subjectService.GetAllByTeacherId(teacherId);
        }

        // is this correct?
        [HttpGet("StudentsAttending/{subjectId}")]
        public List<SubjectViewModel> StudentsAttending(string subjectId)
        {
            return subjectService.GetStudentsAttending(subjectId);
        }

        [HttpGet("GetById/{id}")]
        public SubjectViewModel GetById([FromRoute] string id)
        {
            return subjectService.GetSubjectById(id);
        }

        // fix later
        [HttpGet("Create")]
        public IActionResult AddSubject()
        {
            return this.View();
        }

        [HttpPost("Create")]
        public void AddSubject([FromBody] SubjectInputModel inputModel)
        {
            subjectService.CreateSubject(inputModel);
        }

        [HttpPut("EditSubject/{id}")]
        public SubjectViewModel EditSubject([FromRoute] string id, [FromBody] SubjectInputModel inputModel)
        {
            return subjectService.EditSubject(id, inputModel);
        }

        [HttpPost("AddTeacherToSubject/{subjectId}")]
        public void AddTeacherToSubject([FromRoute] string subjectId, [FromBody] string teacherId)
        {
            subjectService.AddTeacherToSubject(subjectId, teacherId);
        }

        [HttpDelete("RemoveTeacherFromSubject/{subjectId}")]
        public void RemoveTeacherFromSubject([FromRoute] string subjectId, [FromBody] string teacherId)
        {
            subjectService.RemoveTeacherFromSubject(subjectId, teacherId);
        }

        [HttpDelete("DeleteSubject/{id}")]
        public void DeleteSubject([FromRoute] string id)
        {
            subjectService.DeleteSubject(id);
        }
    }
}
