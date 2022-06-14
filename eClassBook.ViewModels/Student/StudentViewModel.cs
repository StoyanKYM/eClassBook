using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Student
{
    public class StudentViewModel
    {
        
        public int StartYear { get; set; }
        public string ClassId { get; set; }

        public string ParentId { get; set; }
        public string SchoolUserId { get; set; }

        public string FullName { get; set; }

        //public ICollection<StudentToGrade> Grades { get; set; }

        //public ICollection<Absence> Absences { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Town { get; set; }

        public string SchoolId { get; set; }
    }
}
