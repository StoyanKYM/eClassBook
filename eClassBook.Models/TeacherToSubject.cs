using eClassBook.Models.SchoolUserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models
{
    public class TeacherToSubject
    {
        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        [ForeignKey(nameof(Subject))]
        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

    }
}
