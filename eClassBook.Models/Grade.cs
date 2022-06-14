using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models
{
    public class Grade
    {
        public Grade()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Students = new HashSet<StudentToGrade>();
        }

        [Key]
        public string Id { get; set; }

        public int ValueNum { get; set; }

        public string ValueWord { get; set; }

        public ICollection<StudentToGrade> Students { get; set; }
    }
}
