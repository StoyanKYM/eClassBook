using eClassBook.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.Subject
{
    public class SubjectViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Grade { get; set; }

        // is this correct?
        public ICollection<UserViewModel> Teachers { get; set; } = new List<UserViewModel>();

    }
}
