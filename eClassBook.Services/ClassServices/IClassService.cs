using eClassBook.ViewModels.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.ClassServices
{
    public interface IClassService
    {
        List<ClassViewModel> GetAll();
        List<ClassViewModel> GetAllBySchool(string schoolId);

        List<ClassViewModel> GetAllByGrade(int grade);

        List<ClassViewModel> GetClassesWithoutClassTeacher(string schoolId);

        ClassViewModel GetClassById(string id);

        void CreateClass(ClassInputModel inputModel);

        void AddClassTeacher(string classId, string teacherId);

        void AddSubject(string classId, ClassToSubjectInputModel inputModel);

        void EditSubject(string classId, ClassToSubjectInputModel inputModel);

        void RemoveSubject(string classId, string subjectId);

        ClassViewModel EditClass(string id, ClassInputModel inputModel);

        
    }
}
