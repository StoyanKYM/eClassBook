using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Student
{
    public class StudentDialogViewModel
    {
        public string SchoolUserId { get; set; }

        public string FullName { get; set; }

        public string Grade { get; set; }

        public string Address { get; set; }

        public int StartYear { get; set; }

        public double AvgScore { get; set; }

        public IDictionary<string, int> Absences { get; set; }

        public string ParentName { get; set; }

        public string Email { get; set; }
    }
}
