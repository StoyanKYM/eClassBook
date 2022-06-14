using eClassBook.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.SubjectService
{
    public interface ISubjectService
    {
        List<SubjectViewModel> GetAll();

        List<SubjectViewModel> GetAllByGradeYear(int grade);

        List<SubjectViewModel> GetAllByTeacherId(string teacherId);

        List<SubjectViewModel> GetStudentsAttending(string subjectId);

        SubjectViewModel GetSubjectById(string id);

        void CreateSubject(SubjectInputModel inputModel);

        void EditSubject(string id, SubjectInputModel inputModel);

        void AddTeacherToSubject(string subjectId, string teacherId);

        void RemoveTeacherFromSubject(string subjectId, string teacherId);

        void DeleteSubject(string id);
    }
}
