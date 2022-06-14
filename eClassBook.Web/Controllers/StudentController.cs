using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.StudentService;
using eClassBook.ViewModels.Absence;
using eClassBook.ViewModels.Grade;
using eClassBook.ViewModels.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Student/")]
    [ApiController]
    [Produces("application/json")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        [HttpGet("Create")]
        public IActionResult CreateStudent()
        {
            return View();
        }
        [HttpPost("Create")]
        public async Task CreateStudent(StudentDTO studentModel)
        {
           await _studentService.AddStudent(studentModel);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllStudents(StudentViewModel studentModel)
        {
            var result = _studentService.GetAllStudents();

            return View(result);
        }

        [HttpGet("GetAllBySchool/{schoolId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public IEnumerable<StudentTableViewModel> GetAllBySchool([FromRoute] string schoolId)
        {
            return _studentService.GetAllStudentsFromSchool(schoolId);
        }

        [HttpGet("GetAllByClass/{classId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal, Teacher")]
        public IEnumerable<StudentDTO> GetAllByClass([FromRoute] string classId)
        {
            return _studentService.GetAllStudentsFromClass(classId);
        }

        [HttpGet("GetById/{id}")]
        public StudentDTO GetById(string id)
        {
            return _studentService.GetStudent(id);
        }


        // TODO: Finish Services

        [HttpGet("GetStudentForDialog/{studentId}")]
        public StudentDialogViewModel GetForDialog(string studentId)
        {
            return _studentService.GetStudentDialogData(studentId);
        }

        // finish later or in post man
        [HttpPost("GradeStudent/{studentId}")]
        public void GradeStudent([FromRoute] string studentId, [FromBody] GradeInputModel gradeModel)
        {
            _studentService.GradeStudent(studentId, gradeModel);
        }
        
        // not finished
        [HttpPut("EditStudentGrade/{gradeId}")]
        public void EditStudentGrade([FromRoute] string gradeId, [FromBody] string newGradeId)
        {
            _studentService.EditGrade(gradeId, newGradeId);
        }

        [HttpDelete("DeleteStudentGrade/{gradeId}")]
        public void DeleteStudentGrade([FromRoute] string gradeId)
        {
            _studentService.RemoveGrade(gradeId);
        }

        // fix later or in post man
        [HttpPost("AddAbsence/{studentId}")]
        public void AddAbsence([FromRoute] string studentId, [FromBody] AbsenceInputModel absenceModel)
        {
            _studentService.AddAbsenceToStudent(studentId, absenceModel);
        }

        [HttpPut("ExcuseAbsence/{studentId}")]
        public void ExcuseAbsence([FromRoute] string studentId, [FromBody] string absenceId)
        {
            _studentService.ExcuseStudentAbsence(studentId, absenceId);
        }

        [HttpPut("UpdateStudent/{studentId}")]
        public void Update([FromRoute] string studentId, [FromBody] StudentEditInputModel editModel)
        {
            _studentService.UpdateStudent(studentId, editModel);
        }

        [HttpDelete("DeleteStudent/{studentId}")]
        public void Delete([FromRoute] string studentId)
        {
            _studentService.RemoveStudent(studentId);
        }
    }
}
