using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.TeacherService;
using eClassBook.ViewModels.Teacher;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Teacher/")]
    public class TeacherController : Controller
    {
        private ITeacherService teacherService;

        public TeacherController(
            ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        public async Task Create([FromBody] TeacherDTO teacherModel)
        {
            await teacherService.AddTeacher(teacherModel);
        }

        [HttpGet("GetAllTeachersFromSchool/{schoolId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public IEnumerable<TeacherTableViewModel> GetAllTeachersFromSchool([FromRoute] string schoolId)
        {
            return this.teacherService.GetAllTeachersFromSchool(schoolId);
        }

        [HttpGet("GetAllTeachersFromSchoolDropdown/{schoolId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public IEnumerable<MinimalSchoolUserDTO> GetAllTeachersFromSchoolDropdown([FromRoute] string schoolId)
        {
            return this.teacherService.GetAllTeachersFromSchoolDropdown(schoolId);
        }

        [HttpGet("GetTeachersListFromSubject/{subjectId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public IEnumerable<MinimalSchoolUserDTO> GetTeachersListFromSubject([FromRoute] string subjectId)
        {
            return this.teacherService.GetTeachersListFromSubject(subjectId);
        }

        [HttpGet("GetAllUnassignedToClass/{schoolId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public IEnumerable<MinimalSchoolUserDTO> GetAllUnassignedToClass([FromRoute] string schoolId)
        {
            return this.teacherService.GetAllUnassignedToClass(schoolId);
        }

        [HttpGet("GetTeacherDialogData/{teacherId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public TeacherDialogViewModel GetTeacherDialogData([FromRoute] string teacherId)
        {
            return this.teacherService.GetTeacherDialogData(teacherId);
        }

        
    }
}
