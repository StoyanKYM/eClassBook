using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models.SchoolUserEntities
{
    public class Parent : SchoolUser
    {
        public Parent()
        {
            this.Children = new HashSet<Student>();
        }

        public ICollection<Student> Children { get; set; }
    }
}
