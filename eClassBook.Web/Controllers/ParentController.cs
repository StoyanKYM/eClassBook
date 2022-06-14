using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.ParentService;
using eClassBook.ViewModels.Parent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Parent/")]
    [ApiController]
    [Produces("application/json")]
    public class ParentController : Controller
    {
        private readonly IParentService parentService;

        public ParentController(
            IParentService parentService)
        {
            this.parentService = parentService;
        }

        [HttpGet("GetAllFromSchool/{schoolId}")]
        public IEnumerable<ParentViewModel> GetAllParentsFromSchool(string schoolId)
        {
            return parentService.GetAllParentsFromSchool(schoolId);
        }

        [HttpGet("GetParent/{parentId}")]
        public ParentViewModel GetParentDialogData(string parentId)
        {
            return parentService.GetParentDialogData(parentId);
        }

        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost("Create")]
        public async Task Create([FromBody] ParentDTO parentModel)
        {
            await parentService.AddParent(parentModel);
        }
    }
}
