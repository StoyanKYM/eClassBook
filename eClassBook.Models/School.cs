using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models
{
    public class School
    {
        public School()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Classes = new HashSet<Class>();
        }

        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public int Number { get; set; }

        public string Address { get; set; }

        public ICollection<Class> Classes { get; set; }
    }
}
