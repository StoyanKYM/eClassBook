using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.SchoolAdminService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("SchoolAdmin/")]
    [ApiController]
    [Produces("application/json")]
    public class SchoolAdminController : Controller
    {
        private readonly ISchoolAdminService schoolAdminService;

        public SchoolAdminController(
            ISchoolAdminService schoolAdminService)
        {
            this.schoolAdminService = schoolAdminService;
        }
        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        [HttpGet("RegisterSchoolAdmin")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost("RegisterSchoolAdmin")]
        public async Task Create([FromBody] SchoolAdminDTO schoolAdminModel)
        {
            await schoolAdminService.AddSchoolAdmin(schoolAdminModel);
        }
    }
}
