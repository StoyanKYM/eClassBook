using eClassBook.Models;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.ViewModels.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.SchoolAdminService
{
    public interface ISchoolAdminService
    {
        Task AddSchoolAdmin(SchoolAdminDTO schoolAdminModel);
    }
}
