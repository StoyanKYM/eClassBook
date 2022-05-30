using eClassBook.ViewModels.Class;
using eClassBook.ViewModels.School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.SchoolServices
{
    public interface ISchoolService
    {
        void CreateSchool(SchoolInputModel inputModel);

        void CreateSchoolSecond(SchoolInputModel inputModel);
        void EditSchool(string schoolId, SchoolInputModel inputModel);

        void DeleteSchool(string schoolId);

        SchoolViewModel GetSchoolById(string schoolId);

        ICollection<SchoolViewModel> GetAll();

    }
}
