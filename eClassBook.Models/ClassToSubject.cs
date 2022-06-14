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
    public class ClassToSubject
    {
        public ClassToSubject()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(Class))]
        public string ClassId { get; set; }

        public virtual Class Class { get; set; }

        [ForeignKey(nameof(Subject))]
        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [ForeignKey(nameof(Teacher))]
        public string TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }

        public string WeekDay { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
