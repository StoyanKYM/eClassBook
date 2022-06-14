using eClassBook.Data.Repositories;
using eClassBook.Models;
using eClassBook.Models.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace eClassBook.Data.DatabaseSeeder
{
    public class DatabaseInitializer : ISeeder
    {
        private readonly eClassBookDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRepositories _repositories;
        private readonly UserManager<ApplicationUser> _userManager;

        public DatabaseInitializer(eClassBookDbContext dbContext, RoleManager<IdentityRole> roleManager, IRepositories repositories, UserManager<ApplicationUser> userManager)
        {
            this._dbContext = dbContext;
            this._roleManager = roleManager;
            this._repositories = repositories;
            this._userManager = userManager;
        }

        public async Task InitializeSeed()
        {
            await SeedRoles();
            SeedSchools();
            SeedClass();
            _repositories.SaveChanges();
        }

        private async Task SeedRoles()
        {

            var roleNames = Enum.GetNames(typeof(RoleTypes));

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
        private void SeedClass()
        {
            if (_dbContext.Classes.FirstOrDefault() != null)
            {
                return;
            }

            var school = _dbContext.Schools.FirstOrDefault();
            var defaultClass = new Class
            {
                StartYear = 2019,
                Grade = 1,
                GradeLetter = 'A',
            };
            if (school == null) return;
            defaultClass.School = school;
            _dbContext.Classes.Add(defaultClass);
            _dbContext.SaveChanges();
        }

        private void SeedSchools()
        {
            if (_dbContext.Schools.FirstOrDefault() != null)
            {
                return;
            }

            var defaultSchool = new School
            {
                Name = "New Bulgarian University School",
                Number = 0,
                Address = "Sofia"
            };
            _dbContext.Schools.Add(defaultSchool);
            _dbContext.SaveChanges();
        }

    }
}
