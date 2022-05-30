using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.ViewModels.Parent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.ParentService
{
    public interface IParentService
    {
        Task AddParent(ParentDTO teacher);

        IEnumerable<ParentViewModel> GetAllParentsFromSchool(string schoolId);

        ParentViewModel GetParentDialogData(string parentId);
    }
}
