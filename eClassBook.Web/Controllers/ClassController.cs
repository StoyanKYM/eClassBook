using eClassBook.Services.ClassServices;
using eClassBook.ViewModels.Class;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Class/")]
    public class ClassController : Controller
    {
        private readonly IClassService classService;

        public ClassController(IClassService classService)
        {
            this.classService = classService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public void Create(ClassInputModel input)
        {
            this.classService.CreateClass(input);

        }

        [HttpPut("AddClassTeacher/{classId}")]
        public void AddClassTeacher([FromRoute] string classId, [FromBody] string teacherId)
        {
            this.classService.AddClassTeacher(classId, teacherId);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = classService.GetAll();

            return View(result);
        }

        [HttpPut("AddSubjectToClass")]
        public void AddSubjectToClass([FromRoute] string classId, [FromBody] ClassToSubjectInputModel inputModel)
        {
            this.classService.AddSubject(classId, inputModel);
        }

        [HttpGet("GetBySchool/{schoolId}")]
        public List<ClassViewModel> GetBySchool([FromRoute] string schoolId)
        {
            var result = classService.GetAllBySchool(schoolId);

            return result;
        }

        [HttpPut("EditSubjectInClass/{classId}")]
        public void EditSubjectInClass([FromRoute] string classId, [FromBody] ClassToSubjectInputModel inputModel)
        {
            this.classService.EditSubject(classId, inputModel);
        }

        /*Remove a subject from subjects collection of a class.*/
        [HttpDelete("RemoveSubjectFromClass/{classId}")]
        public void RemoveSubjectFromClass([FromRoute] string classId, [FromBody] string subjectId)
        {
            this.classService.RemoveSubject(classId, subjectId);
        }

        [HttpGet("GetAllByGrade/{grade}")]
        public List<ClassViewModel> GetAllByGrade([FromRoute] int grade)
        {
            var result = classService.GetAllByGrade(grade);

            return result;
        }

        /*Get all classes in school that don't have a class teacher assigned to them.*/
        [HttpGet("Unassigned/{schoolId}")]
        public List<ClassViewModel> GetClassesWithoutClassTeacher([FromRoute] string schoolId)
        {
            var result = classService.GetClassesWithoutClassTeacher(schoolId);

            return result;
        }

        /*Edit class data. Doesn't change class teacher.*/
        [HttpPut("EditClass/{id}")]
        public ClassViewModel EditClass([FromRoute] string id, [FromBody] ClassInputModel inputModel)
        {
            return this.classService.EditClass(id, inputModel);
        }

        [HttpGet("GetById/{id}")]
        public ClassViewModel GetById([FromRoute] string id)
        {
            var result = this.classService.GetClassById(id);

            return result;
        }
    }
}
