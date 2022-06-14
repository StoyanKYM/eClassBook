using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Class
{
    public class ClassToSubjectInputModel
    {
        [Required]
        public string SubjectId { get; set; }

        [Required]
        public string TeacherId { get; set; }

        [Required]
        public string WeekDay { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }
    }
}
