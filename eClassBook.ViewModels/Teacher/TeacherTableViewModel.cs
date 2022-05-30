using eClassBook.ViewModels.Subject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Teacher
{
    public class TeacherTableViewModel
    {
        public string SchoolUserId { get; set; }

        public string FullName { get; set; }

        public string Grade { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public ICollection<SubjectViewModel> Subjects { get; set; }

        public double AvgScore { get; set; }
    }
}
