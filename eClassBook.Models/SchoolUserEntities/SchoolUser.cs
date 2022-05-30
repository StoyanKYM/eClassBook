using eClassBook.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models.SchoolUserEntities
{
    public class SchoolUser
    {
        public SchoolUser()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }
        // What is PIN?
        public string Pin { get; set; }

        public string Address { get; set; }

        public string Town { get; set; }

        [ForeignKey(nameof(School))]
        public string SchoolId { get; set; }
        public virtual School School { get; set; }

        public ApplicationUser User { get; set; }

        public RoleTypes Role { get; set; }
    }
}
