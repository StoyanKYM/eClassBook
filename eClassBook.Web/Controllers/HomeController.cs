using eClassBook.Data.DatabaseSeeder;
using eClassBook.Models;
using eClassBook.Services.HomeServices;
using eClassBook.Services.SchoolUserService;
using eClassBook.ViewModels.Home;
using eClassBook.ViewModels.User;
using eClassBook.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eClassBook.Web.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        private readonly IGetCountService _countService;
        private readonly ISchoolUserService _schoolUserService;
        private readonly ISeeder _seeder;
        
        public HomeController(ILogger<HomeController> logger, IGetCountService countService, ISeeder seeder, ISchoolUserService schoolUserService)
        {
            this._logger = logger;
            this._countService = countService;
            this._seeder = seeder;
            this._schoolUserService = schoolUserService;
        }

        public IActionResult Index()
        {
            var counts = _countService.GetCount();

            var viewModel = new IndexViewModel() 
            { 
                SubjectCount = counts.SubjectCount,
                SchoolCount = counts.SchoolCount,
                StudentCount = counts.StudentCount,
            };

            
            _seeder.InitializeSeed();

            return View(viewModel);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserViewModel>> GetAll()
        {
            try
            {
                var schoolUsers = _schoolUserService.GetAllSchoolUsers();
                return Ok(schoolUsers);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserViewModel> GetById(string id)
        {
            return _schoolUserService.GetSchoolUserById(id);
        }

        [HttpGet("getSchool/{schoolId}")]
        public ActionResult<School> GetBySchoolId(string schoolId)
        {
            return _schoolUserService.GetSchool(schoolId);
        }
    }
}