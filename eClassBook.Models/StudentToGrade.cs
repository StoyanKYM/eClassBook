using eClassBook.Models.SchoolUserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models
{
    public class StudentToGrade
    {
        public StudentToGrade()
        {
            this.Id = Guid.NewGuid().ToString();

        }

        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Student))]
        public string StudentId { get; set; }

        public virtual Student Student { get; set; }

        [ForeignKey(nameof(Grade))]
        public string GradeId { get; set; }

        public virtual Grade Grade { get; set; }

        [ForeignKey(nameof(Subject))]
        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
