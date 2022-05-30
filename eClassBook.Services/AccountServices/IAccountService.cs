using eClassBook.Models;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.ViewModels.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.AccountServices
{
    public interface IAccountService
    {
        Task<ApplicationUser> Register(FullRegisterInputModel inputModel);

        //Task SeedAdmin(RegisterInputModel model);

        Task<ApplicationUser> RegisterSchoolUser(SchoolUser schoolUser);
    }
}
