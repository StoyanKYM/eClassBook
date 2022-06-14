using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Class
{
    public class ClassInputModel
    {
        
        [Required]
        public int StartYear { get; set; }

        /// Renamed from Grade
        
        [Required]
        [Range(1, 12)]
        public int GradeYear { get; set; }

        [Required]
        public char GradeLetter { get; set; }

        [Required]
        public string SchoolId { get; set; }

        [Required]
        public string ClassTeacherId { get; set; }

        //public ICollection<ClassToSubject> Subjects { get; set; }
    }
}
