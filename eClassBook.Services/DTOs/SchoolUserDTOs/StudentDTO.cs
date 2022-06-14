using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.DTOs.SchoolUserDTOs
{
    public class StudentDTO : SchoolUserDTO
    {
        [Required]
        public string ClassId { get; set; }

        [Required]
        public int StartYear { get; set; }
    }
}
