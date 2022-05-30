using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.DTOs
{
    public class GetSchoolDTO
    {
        [Key]
        public string Id { get; set; }

        [Display(Name = "School Name")]
        public string Name { get; set; }
    }
}
