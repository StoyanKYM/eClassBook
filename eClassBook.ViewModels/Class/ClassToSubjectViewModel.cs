using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Class
{
    public class ClassToSubjectViewModel
    {
        public string Id { get; set; }

        public string SubjectName { get; set; }

        public string Grade { get; set; }

        public string TeacherName { get; set; }

        public string WeekDay { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
