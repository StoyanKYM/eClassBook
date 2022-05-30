using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models
{
    public class Subject
    {
        public Subject()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Classes = new HashSet<ClassToSubject>();
            this.Teachers = new HashSet<TeacherToSubject>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public int GradeYear { get; set; }

        public ICollection<ClassToSubject> Classes { get; set; } // Всеки subject има много класове в които се изучава

        public ICollection<TeacherToSubject> Teachers { get; set; } = new List<TeacherToSubject>(); // Всеки subject има много учители които го преподават
    }
}
