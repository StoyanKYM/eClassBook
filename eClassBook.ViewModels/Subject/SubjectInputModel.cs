using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Subject
{
    public class SubjectInputModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 12)]
        public int GradeYear { get; set; }
    }
}
