using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.TeacherService;
using eClassBook.ViewModels.Teacher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Teacher/")]
    [ApiController]
    [Produces("application/json")]
    public class TeacherController : Controller
    {
        private ITeacherService teacherService;

        public TeacherController(
            ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task Create([FromBody] TeacherDTO teacherModel)
        {
            await teacherService.AddTeacher(teacherModel);
            
        }

        [HttpGet("GetAllTeachersFromSchool/{schoolId}")]
        public IEnumerable<TeacherTableViewModel> GetAllTeachersFromSchool([FromRoute] string schoolId)
        {
            return this.teacherService.GetAllTeachersFromSchool(schoolId);
        }

        [HttpGet("GetAllTeachersFromSchoolDropdown/{schoolId}")]
        public IEnumerable<MinimalSchoolUserDTO> GetAllTeachersFromSchoolDropdown([FromRoute] string schoolId)
        {
            return this.teacherService.GetAllTeachersFromSchoolDropdown(schoolId);
        }

        [HttpGet("GetTeachersListFromSubject/{subjectId}")]
        public IEnumerable<MinimalSchoolUserDTO> GetTeachersListFromSubject([FromRoute] string subjectId)
        {
            return this.teacherService.GetTeachersListFromSubject(subjectId);
        }

        [HttpGet("GetAllUnassignedToClass/{schoolId}")]
        public IEnumerable<MinimalSchoolUserDTO> GetAllUnassignedToClass([FromRoute] string schoolId)
        {
            return this.teacherService.GetAllUnassignedToClass(schoolId);
        }

        [HttpGet("GetTeacherDialogData/{teacherId}")]
        public TeacherDialogViewModel GetTeacherDialogData([FromRoute] string teacherId)
        {
            return this.teacherService.GetTeacherDialogData(teacherId);
        }

        
    }
}
