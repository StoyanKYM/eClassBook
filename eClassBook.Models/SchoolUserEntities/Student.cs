using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models.SchoolUserEntities
{
    public class Student : SchoolUser
    {
        public Student()
        {
            this.Grades = new HashSet<StudentToGrade>();
            this.Absences = new HashSet<Absence>();
        }

        public int StartYear { get; set; }

        [ForeignKey(nameof(Class))]
        public string ClassId { get; set; }
        public virtual Class Class { get; set; }

        [ForeignKey(nameof(Parent))]
        public string ParentId { get; set; }
        public virtual Parent Parent { get; set; }

        public ICollection<StudentToGrade> Grades { get; set; }

        public ICollection<Absence> Absences { get; set; }
    }
}
