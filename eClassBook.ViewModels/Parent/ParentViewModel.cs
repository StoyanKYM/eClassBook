using eClassBook.ViewModels.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Parent
{
    public class ParentViewModel
    {
        public string SchoolUserId { get; set; }

        public string FullName { get; set; }

        public string Address { get; set; }

        public ICollection<string> Children { get; set; }

        public string Email { get; set; }

        public ICollection<StudentDialogViewModel> ChildrenData { get; set; } = new List<StudentDialogViewModel>();
    }
}
