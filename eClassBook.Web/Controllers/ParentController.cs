using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.ParentService;
using eClassBook.ViewModels.Parent;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Parent/")]
    public class ParentController : Controller
    {
        private readonly IParentService parentService;

        public ParentController(
            IParentService parentService)
        {
            this.parentService = parentService;
        }

        [HttpGet("GetAllFromSchool/{schoolId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public IEnumerable<ParentViewModel> GetAllParentsFromSchool(string schoolId)
        {
            return parentService.GetAllParentsFromSchool(schoolId);
        }

        [HttpGet("GetParent/{parentId}")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin, Principal")]
        public ParentViewModel GetParentDialogData(string parentId)
        {
            return parentService.GetParentDialogData(parentId);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        public async Task Create([FromBody] ParentDTO parentModel)
        {
            await parentService.AddParent(parentModel);
        }
    }
}
