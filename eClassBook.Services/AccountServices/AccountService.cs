using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.Models.Enumerations;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.ViewModels.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eClassBook.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRepositories repo;

        public AccountService(
            IRepositories repositories,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this.repo = repositories;
        }
        
        public async Task<ApplicationUser> Register(FullRegisterInputModel inputModel)
        {
            var email = GenerateEmail(inputModel.FirstName, inputModel.LastName);
            ;

            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = email,
                UserName = email,
                RoleName = inputModel.RoleName
            };

            var roles = Enum.GetValues(typeof(RoleTypes));
            var role = roles.GetValue(0);

            foreach (var r in roles)
            {
                if (role.ToString() == inputModel.RoleName) break;
                if (r.ToString() == inputModel.RoleName)
                {
                    role = r;
                }
            }

            try
            {
                await _userManager.CreateAsync(user, inputModel.Pin);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
              
            await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleTypes), role));

            

            return user;
        }

        public async Task<ApplicationUser> RegisterSchoolUser(SchoolUser schoolUser)
        {
            var accountRegister = new FullRegisterInputModel
            {
                Pin = schoolUser.Pin,
                FirstName = schoolUser.FirstName,
                LastName = schoolUser.LastName,
                RoleName = schoolUser.Role.ToString()
            };
            return await Register(accountRegister);
        }

        private string GenerateEmail(string firstName, string lastName)
        {
            var emailPrefix = firstName.Substring(0, 1).ToLower() + lastName.ToLower();
            var counter = repo.Users.Query().AsNoTracking().Count(u => u.Email.Contains(emailPrefix));

            if (counter >= 1)
            {
                emailPrefix = emailPrefix + counter;
            }

            return emailPrefix + "@eClassBook.nbu";

        }
    }
}
