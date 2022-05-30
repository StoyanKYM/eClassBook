using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.SchoolAdminService;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("SchoolAdmin/")]
    public class SchoolAdminController : Controller
    {
        private readonly ISchoolAdminService schoolAdminService;

        public SchoolAdminController(
            ISchoolAdminService schoolAdminService)
        {
            this.schoolAdminService = schoolAdminService;
        }
        [HttpGet("RegisterSchoolAdmin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost("RegisterSchoolAdmin")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        public async Task Create([FromBody] SchoolAdminDTO schoolAdminModel)
        {
            await schoolAdminService.AddSchoolAdmin(schoolAdminModel);
        }
    }
}
