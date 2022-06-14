using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.Services.PrincipalService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Principal/")]
    [ApiController]
    [Produces("application/json")]
    public class PrincipalController : Controller
    {
        private readonly IPrincipalService principalService;

        public PrincipalController(
            IPrincipalService principalService)
        {
            this.principalService = principalService;
        }

        [Authorize(Roles = "SuperAdmin, SchoolAdmin")]
        [HttpGet("Create")]
        public IActionResult CreatePrincipal()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task CreatePrincipal([FromBody] PrincipalDTO principalModel)
        {
            await principalService.AddPrincipal(principalModel);
        }
    }
}
