using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.PrincipalService;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Principal/")]
    public class PrincipalController : Controller
    {
        private readonly IPrincipalService principalService;

        public PrincipalController(
            IPrincipalService principalService)
        {
            this.principalService = principalService;
        }
        [HttpGet("Create")]
        public IActionResult CreatePrincipal()
        {
            return View();
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        public async Task CreatePrincipal([FromBody] PrincipalDTO principalModel)
        {
            await principalService.AddPrincipal(principalModel);
        }
    }
}
