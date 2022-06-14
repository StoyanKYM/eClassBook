using eClassBook.Services.SchoolServices;
using eClassBook.ViewModels.School;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("School/")]
    public class SchoolController : Controller
    {
        private readonly ISchoolService schoolService;

        public SchoolController(ISchoolService schoolService)
        {
            this.schoolService = schoolService;
        }

        [HttpGet("CreateSchool")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("CreateSchool")]
        public IActionResult Create(SchoolInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            schoolService.CreateSchoolSecond(inputModel);

            return this.Redirect("/");
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = schoolService.GetAll();

            return View(result);
        }

        [HttpGet("GetById/{id}")]
        public SchoolViewModel GetById([FromRoute] string id)
        {
            return schoolService.GetSchoolById(id);
        }

        // Create Form
        [HttpPut("EditSchool/{id}")]
        public void EditSchool([FromRoute] string id, [FromBody] SchoolInputModel inputModel)
        {
            schoolService.EditSchool(id, inputModel);
        }

        [HttpDelete("DeleteSchool/{id}")]
        public IActionResult DeleteSchool([FromRoute] string id)
        {
            schoolService.DeleteSchool(id);

            return this.Redirect("/");
        }

    }
}
