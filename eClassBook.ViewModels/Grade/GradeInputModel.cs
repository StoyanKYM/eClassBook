using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Grade
{
    public class GradeInputModel
    {
        [Required]
        public string GradeId { get; set; }

        [Required]
        public string SubjectId { get; set; }

        [Required]
        public string TeacherId { get; set; }
    }
}
