using eClassBook.Services.SchoolUserService;
using eClassBook.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("SchoolUser/")]
    public class SchoolUserController : Controller
    {
        private readonly ISchoolUserService _schoolUserService;

        public SchoolUserController(ISchoolUserService schoolUserService)
        {
            _schoolUserService = schoolUserService;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<UserViewModel>> GetAll()
        {
            var schoolUsers = _schoolUserService.GetAllSchoolUsers();
            return View(schoolUsers);
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<UserViewModel> GetById(string id)
        {
            return _schoolUserService.GetSchoolUserById(id);
        }

    }
}
