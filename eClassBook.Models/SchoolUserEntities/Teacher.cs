using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models.SchoolUserEntities
{
    public class Teacher : SchoolUser
    {
        public Teacher()
        {
            this.Subjects = new HashSet<TeacherToSubject>();
        }

        public ICollection<TeacherToSubject> Subjects { get; set; }
    }
}
