using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Teacher
{
    public class TeacherDialogViewModel : TeacherTableViewModel
    {
        public string Email { get; set; }

        //public ICollection<SubjectOnlyViewModel> Subjects { get; set; }

        public double AvgScore { get; set; }
    }
}
