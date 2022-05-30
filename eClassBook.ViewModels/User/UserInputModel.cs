using eClassBook.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eClassBook.ViewModels.User
{
    public class UserInputModel
    {

        public UserInputModel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Second Name ")]
        public string SecondName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Last Name ")]
        public string LastName { get; set; }
        // What is PIN?
        [Required]
        [Display(Name = "Pin")]
        public string Pin { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        public string Town { get; set; }

        [Required]
        public RoleTypes RoleType { get; set; }

        [Display(Name = "Schools: ")]
        public List<SelectListItem> SchoolItems { get; set; }

    }
}
