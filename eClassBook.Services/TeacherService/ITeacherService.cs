using eClassBook.Services.DTOs.SchoolUserDTOs;
using eClassBook.ViewModels.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.TeacherService
{
    public interface ITeacherService
    {
        Task AddTeacher(TeacherDTO teacher);

        IEnumerable<TeacherTableViewModel> GetAllTeachersFromSchool(string schoolId);

        IEnumerable<MinimalSchoolUserDTO> GetAllTeachersFromSchoolDropdown(string schoolId);

        IEnumerable<MinimalSchoolUserDTO> GetTeachersListFromSubject(string subjectId);

        IEnumerable<MinimalSchoolUserDTO> GetAllUnassignedToClass(string schoolId);

        TeacherDialogViewModel GetTeacherDialogData(string teacherId);
    }
}
