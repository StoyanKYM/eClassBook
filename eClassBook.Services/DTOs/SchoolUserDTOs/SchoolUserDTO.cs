using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.DTOs.SchoolUserDTOs
{
    public class SchoolUserDTO
    {
        [Required]
        public string FirstName { get; set; }

        public string SecondName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Pin { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public string SchoolId { get; set; }
    }
}
