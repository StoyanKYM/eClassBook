using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.ViewModels.School
{
    public class SchoolInputModel
    {
        [Required]
        [MinLength(3)]
        [Display(Name = "Name of School")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Number of School")]
        public int Number { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Address of School")]
        public string Address { get; set; }
    }
}
