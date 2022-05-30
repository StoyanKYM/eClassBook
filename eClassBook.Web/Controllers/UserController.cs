using eClassBook.Data;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.Services.UserServices;
using eClassBook.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web.Mvc;

namespace eClassBook.Web.Controllers
{
    public class UserController : Microsoft.AspNetCore.Mvc.Controller
    {
        
        private readonly eClassBookDbContext dbContext;
        private readonly IUserService userService;
        //private readonly Microsoft.AspNetCore.Identity.UserManager<SchoolUser> userManager;

        public UserController(eClassBookDbContext dbContext, IUserService userService) //Microsoft.AspNet.Identity.UserManager<SchoolUser> userManager)
        {
            //;
            //var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            this.dbContext = dbContext;
            this.userService = userService;
            //this.userManager = userManager;
        }
        //[Authorize]
        public IActionResult Create()
        {
            var userModel = new UserInputModel();

            userModel.SchoolItems = new List<SelectListItem>();

            userModel.SchoolItems.Add(new SelectListItem

            {

                Value = "",

                Text = "Select School",

                Selected = true

            });


            var data = dbContext.Schools;

            foreach (var item in data)

            {

                userModel.SchoolItems.Add(new SelectListItem

                {

                    Value = Convert.ToString(item.Id),

                    Text = item.Name,

                    Selected = true

                });

            }

            return View(userModel);
        }

        //[Authorize]
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Create(UserInputModel input)
        {
            var data = dbContext.Schools;


            if (!this.ModelState.IsValid)
            {
                input.SchoolItems = new List<SelectListItem>();

                input.SchoolItems.Add(new SelectListItem

                {

                    Value = "Mehano",

                    Text = "Select School",

                    Selected = true

                });


                foreach (var item in data)

                {

                    input.SchoolItems.Add(new SelectListItem

                    {

                        Value = Convert.ToString(item.Id),

                        Text = item.Name,

                        Selected = true

                    });

                }

                ViewBag.SelectedValue = input.Id;

                ViewBag.SelectedText = data.Where(m => m.Id == input.Id).FirstOrDefault().Name;

                return this.View(input);

            }

            

            // Add user Id
            //var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var schoolUser = new SchoolUser
            {
                FirstName = input.FirstName,
                SecondName = input.SecondName,
                LastName = input.LastName,
                Address = input.Address,
                Pin = input.Pin,
                Town = input.Town,
                SchoolId = ViewBag.SelectedValue,
                Role = input.RoleType,
            };

            dbContext.SchoolUsers.Add(schoolUser);
            dbContext.SaveChanges();

            
            return this.Redirect("/");
            
        }

        public IActionResult All(int id)
        {


            return this.View();
        }

        
    }

    
}
