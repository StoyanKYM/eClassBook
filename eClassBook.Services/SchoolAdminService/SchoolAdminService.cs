using AutoMapper;
using eClassBook.Data.Repositories;
using eClassBook.Models.SchoolUserEntities;
using eClassBook.Services.AccountServices;
using eClassBook.Services.DTOs.SchoolUserDTOs;
using System.Reflection;

namespace eClassBook.Services.SchoolAdminService
{
    public class SchoolAdminService : ISchoolAdminService
    {
        private readonly IAccountService _accountService;
        private readonly IRepositories repo;
        private readonly IMapper mapper;

        public SchoolAdminService(
            IAccountService accountService,
            IRepositories repo,
            IMapper mapper)
        {
            _accountService = accountService;
            this.repo = repo;
            this.mapper = mapper;
        }

        public async Task AddSchoolAdmin(SchoolAdminDTO schoolAdminModel)
        {
            var admin = mapper.Map<SchoolAdminDTO, SchoolAdmin>(schoolAdminModel);
            var school = repo.Schools.GetById(schoolAdminModel.SchoolId);

            if (school == null)
            {
                throw new TargetException("School not found");
            }

            admin.School = school;
            admin.Role = Models.Enumerations.RoleTypes.SchoolAdmin;

            var account = await _accountService.RegisterSchoolUser(admin);
            if (account == null)
            {
                return;
            }
            admin.User = account;
            admin.Id = account.Id;
            
            repo.SchoolAdmins.Create(admin);
            repo.SaveChanges();
        }
    }
}

