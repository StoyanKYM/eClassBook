using eClassBook.Models;
using eClassBook.Services.AccountServices;
using eClassBook.ViewModels.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eClassBook.Web.Controllers
{
    [Route("Account/")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet("RegisterAccount")]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost("RegisterAccount")]
        public async Task<ActionResult<ApplicationUser>> CreateAccount(FullRegisterInputModel inputModel)
        {
            await _accountService.Register(inputModel);
            return this.Redirect("/");
        }
    }
}
