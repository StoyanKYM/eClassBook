using eClassBook.Models.SchoolUserEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eClassBook.Models
{
    // Паралелка или клас?
    public class Class
    {
        public Class()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Subjects = new HashSet<ClassToSubject>();
        }

        [Key]
        public string Id { get; set; }

        public int StartYear { get; set; }

        /// Renamed from Grade
        public int Grade { get; set; }

        public char GradeLetter { get; set; }

        [ForeignKey(nameof(ClassTeacher))]
        public string ClassTeacherId { get; set; }
        public Teacher ClassTeacher { get; set; }


        [ForeignKey(nameof(School))]
        public string SchoolId { get; set; }

        public virtual School School { get; set; }

        public ICollection<ClassToSubject> Subjects { get; set; }
    }
}
