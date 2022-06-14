using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Services.DTOs.SchoolUserDTOs
{
    public class ParentDTO : SchoolUserDTO
    {
        [Required]
        public IEnumerable<string> ChildrenId { get; set; }
    }
}
