using eClassBook.Models;
using eClassBook.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.SchoolUserService
{
    public interface ISchoolUserService
    {
        UserViewModel GetSchoolUserById(string id);

        IEnumerable<UserViewModel> GetAllSchoolUsers();

        School GetSchool(string schoolId);
    }
}
